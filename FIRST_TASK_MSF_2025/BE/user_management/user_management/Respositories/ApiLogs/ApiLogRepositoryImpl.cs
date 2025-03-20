using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using user_management.Data;
using user_management.Models;

namespace user_management.Respositories.ApiLogs
{
    public class ApiLogRepositoryImpl : IApiLogRepository
    {
        private readonly ApplicationContext _context;

        public ApiLogRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<ApiLog>> GetAllApiLogs()
        {
            return await _context.ApiLogs.FromSqlRaw("EXEC GetApiLogs").AsNoTracking().ToListAsync();
        }

        public async Task SaveApiLog(ApiLog apiLog)
        {
            using (var transation = await _context.Database.BeginTransactionAsync())
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SaveApiLog @Method, @EndPoint, @StatusCode, @IpAddress, @Timestamp, @TimeLimit, @UserName",
                    new SqlParameter("@Method", apiLog.Method),
                    new SqlParameter("@EndPoint", apiLog.Endpoint),
                    new SqlParameter("@StatusCode", apiLog.StatusCode),
                    new SqlParameter("@Ipaddress", apiLog.IpAddress),
                    new SqlParameter("@Timestamp", apiLog.Timestamp),
                    new SqlParameter("@TimeLimit", apiLog.TimeLimit),
                    new SqlParameter("@UserName", apiLog.userName)
                    );

                await transation.CommitAsync();
            }
        }
    }
}
