using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user_management.Dto.RequestDto;
using user_management.Services.Settings;

namespace user_management.Controllers
{
    [Route("api/v1/settings")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            try
            {
                return Ok(await _settingService.GetAllSettings());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching setting: {ex.Message}");
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("update-setting")]
        public async Task<IActionResult> UpdateSetting([FromBody] List<UpdateSettingRequestDto> settingRequestDtos) 
        {
            try
            {
                return Ok(await _settingService.UpdateSettingAsync(settingRequestDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Updating setting: {ex.Message}");
            }
        }

        [HttpGet("setting-redis")]
        public async Task<IActionResult> GetSettingFromRedis()
        {
            try
            {
                var result = await _settingService.GetSettingsFromRedisAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting settings from setting service: {ex.Message}");
            }
        }
    }
}
