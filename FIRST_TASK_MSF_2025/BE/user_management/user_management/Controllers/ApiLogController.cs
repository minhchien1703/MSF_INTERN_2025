using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user_management.Models;
using user_management.Services.ApiLogService;

namespace user_management.Controllers
{
    [Route("api/v1/apilogs")]
    [ApiController]
    public class ApiLogController : ControllerBase
    {
        private readonly ApiLogService _apiLogService;

        public ApiLogController(ApiLogService apiLogService)
        {
            _apiLogService = apiLogService;
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ApiLog>>> GetAllApiLog()
        //{
        //    try
        //    {
        //        return Ok(await _apiLogService.GetApiLogs());
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error fetching apilog: {ex.Message}");
        //    }
        //}

    }
}
