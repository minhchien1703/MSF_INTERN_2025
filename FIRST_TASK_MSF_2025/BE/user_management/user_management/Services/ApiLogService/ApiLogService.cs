using user_management.Models;
using user_management.Respositories.ApiLogs;

namespace user_management.Services.ApiLogService
{
    public class ApiLogService
    {
        private readonly IApiLogRepository _logRepository;

        public ApiLogService(IApiLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        //public async Task<List<ApiLog>> GetApiLogs()
        //{
        //    try
        //    {
        //        return await _logRepository.GetAllApiLogs();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error get apiLogs by repo: {ex.Message}");
        //        throw new Exception($"Error get apiLogs by repo: {ex.Message}");
        //    }
        //}
    }
}
