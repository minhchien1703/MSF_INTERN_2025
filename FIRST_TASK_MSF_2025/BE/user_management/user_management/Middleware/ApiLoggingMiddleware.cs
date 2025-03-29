using System.Diagnostics;
using user_management.Models;
using user_management.Respositories.ApiLogs;

namespace user_management.Middleware
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public ApiLoggingMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var originalResponseBody = context.Response.Body;
            var stopwatch = Stopwatch.StartNew();
            var userName = context.Session.GetString("UserName") ?? "Anonymous";

            try
            {
                await _next(context);
                stopwatch.Stop();

                using (var scope = _scopeFactory.CreateScope())
                {
                    var logRepository = scope.ServiceProvider.GetRequiredService<IApiLogRepository>();

                    var log = new ApiLog
                    {
                        //Method = request.Method,
                        //Endpoint = request.Path,
                        //userName = userName,
                        TimeLimit = stopwatch.Elapsed.TotalSeconds.ToString() + " ms",
                        //StatusCode = context.Response.StatusCode,
                        IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                        Timestamp = DateTime.UtcNow
                    };

                    //await logRepository.SaveApiLog(log);
                }
            }
            finally
            {
                context.Response.Body = originalResponseBody;
            }
        }
    }

}

