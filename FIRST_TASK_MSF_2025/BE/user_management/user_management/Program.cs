using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using user_management.Data;
using user_management.Mappers;
using user_management.Middleware;
using user_management.Respositories.ApiLogs;
using user_management.Respositories.Authen;
using user_management.Respositories.BlackListToken;
using user_management.Respositories.RoleService;
using user_management.Respositories.Settings;
using user_management.Respositories.UserRepository;
using user_management.Services.ApiLogService;
using user_management.Services.AuthenService;
using user_management.Services.Role;
using user_management.Services.Settings;
using user_management.Services.UserService;
using user_management.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuthenRepository, AuthenticationRepositoryImpl>();
builder.Services.AddScoped<IAuthenService, AuthenServiceImpl>();
builder.Services.AddScoped<BasicMapper>();
builder.Services.AddScoped<IBlackListTokenRepository, BlackListTokenRepositoryImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IRoleRepository, RoleRepositoryImpl>();
builder.Services.AddScoped<IRoleService, RoleServiceImpl>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();
builder.Services.AddScoped<ISettingService, SettingServiceImpl>();
builder.Services.AddScoped<IApiLogRepository, ApiLogRepositoryImpl>();
builder.Services.AddScoped<ApiLogService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cấu hình Cord cho phép request các port bên ngoài
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//Cấu hình sql server
builder.Services.AddDbContext<ApplicationContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("UserManagement"));
});

//Cấu hình redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConnection = "localhost:6379,abortConnect=false";
    return ConnectionMultiplexer.Connect(redisConnection);
});

// authen
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var secretKey = builder.Configuration["AppSettings:SecretKey"];
Console.WriteLine($"SecretKey: {secretKey ?? "NULL"}");
var key = Encoding.UTF8.GetBytes(secretKey);

// cấu hình JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(options =>
   {
       options.RequireHttpsMetadata = true;
       options.SaveToken = true;
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(key),
           ValidateIssuer = false, // Có thể thay đổi thành true nếu bạn sử dụng Issuer
           ValidateAudience = false, // Có thể thay đổi thành true nếu bạn sử dụng Audience
           ValidateLifetime = true,
           ClockSkew = TimeSpan.Zero // Tùy chọn: bỏ qua thời gian trễ mặc định của JWT
       };

       // Middleware kiểm tra token có trong blacklist không
       options.Events = new JwtBearerEvents
       {
           OnMessageReceived = async context =>
           {
               var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
               if (!string.IsNullOrEmpty(token))
               {
                   using var scope = context.HttpContext.RequestServices.CreateScope();
                   var blackListRepo = scope.ServiceProvider.GetRequiredService<IBlackListTokenRepository>();

                   if (await blackListRepo.IsTokenBlackListed(token))
                   {
                       context.Fail("Token is blacklisted!");
                   }
               }
           }
       };
   });

//Cấu hình cache   
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1440); // Timeout session sau 30 phút
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();
//Load settings into redis if start app.
using (var scope = app.Services.CreateScope())
{
    var settingService = scope.ServiceProvider.GetRequiredService<ISettingService>();
    try
    {
        await settingService.LoadSettingsToRedisAsync();
        Console.WriteLine("✅ Settings loaded into Redis successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Error loading settings into Redis: {ex.Message}");
    }
}

// Thêm Middleware vào pipeline
app.UseMiddleware<ApiLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
