using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Models;

namespace user_management.Services.Settings
{
    public interface ISettingService
    {
        Task<List<Setting>> GetAllSettings();
        bool CheckMaximumLength(string password, int maxLength);
        bool CheckNumerial(string password);
        bool CheckUppercase(string password);
        bool CheckSpecialCharacter(string password);
        Task LoadSettingsToRedisAsync();
        Task<MessageResponseDto> UpdateSettingAsync(List<UpdateSettingRequestDto> settingRequestDtos);
        Task<Dictionary<string, string>> GetSettingsFromRedisAsync();
    }
}
