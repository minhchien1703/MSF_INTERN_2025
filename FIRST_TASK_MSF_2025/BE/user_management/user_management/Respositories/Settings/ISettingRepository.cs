using user_management.Dto.ResponseDto;
using user_management.Models;

namespace user_management.Respositories.Settings
{
    public interface ISettingRepository
    {
        Task<List<Setting>> GetSettings();
        Task<MessageResponseDto> UpdateSetting(int id, string newValue);
    }
}
