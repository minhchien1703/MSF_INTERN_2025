using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using user_management.Data;
using user_management.Dto.RequestDto;
using System.Text;
using System.Security.Claims;
using user_management.Dto.ResponseDto;
using user_management.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace user_management.Respositories.Authen
{
    public class AuthenticationRepositoryImpl : IAuthenRepository
    {
        private readonly int HASHPASSWORD = 10;
        private readonly string _secretKey;
        private readonly int _expiresInDay;
        private readonly BasicMapper _mapper;
        private readonly ApplicationContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;


        public AuthenticationRepositoryImpl(
            ApplicationContext applicationContext,
            IConfiguration configuration,
            BasicMapper mapper,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = applicationContext;
            _configuration = configuration;
            _secretKey = configuration["AppSettings:SecretKey"];
            _expiresInDay = 24;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public LoginResponseDto login(LoginRequestDto req)
        {
            try
            {
                var user = _context.Users.Where(user => user.Email.Equals(req.Email)).FirstOrDefault();
                if (user != null)
                {
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(req.Password, user.Password);
                    var role = _context.Roles.Find(user.Role_Id);

                    if (isPasswordValid)
                    {
                        var key = Encoding.ASCII.GetBytes(_secretKey);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name, user.Id.ToString()),
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim(ClaimTypes.Role, role.Name)
                            }),
                            Expires = DateTime.UtcNow.AddDays(_expiresInDay),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var securityTokenHandler = new JwtSecurityTokenHandler();
                        var token = securityTokenHandler.CreateToken(tokenDescriptor);
                        var tokenString = securityTokenHandler.WriteToken(token);

                        //Save info user to session
                        var httpContext = _httpContextAccessor.HttpContext;
                        httpContext.Session.SetString("UserId", user.Id.ToString());
                        httpContext.Session.SetString("UserEmail", user.Email);
                        httpContext.Session.SetString("UserRole", role.Name);

                        return new LoginResponseDto
                        {
                            UserName = user.LastName,
                            RoleName = role.Name,
                            Token = tokenString
                        };

                    }
                    else
                    {
                        throw new UnauthorizedAccessException("Valid password!");
                    }
                }
                else
                {
                    throw new ArgumentException("User is not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
                throw new NotImplementedException();
            }
        }

        public async Task<MessageResponseDto> Register(RegisterRequestDto req)
        {
            using (var transation = await _context.Database.BeginTransactionAsync())
            {
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);

                var messageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SaveUser @FirstName, @LastName, @Email, @Password, @WorkUnit, @ResponsibleUnit, @Status, @RoleId, @Message OUTPUT",
                    new SqlParameter("@FirstName", req.FirstName),
                    new SqlParameter("@LastName", req.LastName),
                    new SqlParameter("@Email", req.Email),
                    new SqlParameter("@Password", hashPassword),
                    new SqlParameter("@WorkUnit", req.WorkUnit),
                    new SqlParameter("@ResponsibleUnit", req.ResponsibleUnit),
                    new SqlParameter("@Status", req.Status),
                    new SqlParameter("@RoleId", 2),
                    messageParam
                );

                await transation.CommitAsync();

                return new MessageResponseDto
                {
                    Message = messageParam.Value.ToString()
                };
            }
        }



    }
}
