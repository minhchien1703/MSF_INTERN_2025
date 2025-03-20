using StackExchange.Redis;
using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Mappers;
using user_management.Models;
using user_management.Respositories.Settings;

namespace user_management.Services.Settings
{
    public class SettingServiceImpl : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly BasicMapper _mapper;
        private readonly IDatabase _RedisDb;
        private const string SETTING_KEY = "app:settings";


        public SettingServiceImpl(ISettingRepository settingRepository, BasicMapper mapper, IConnectionMultiplexer redis)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
            _RedisDb = redis.GetDatabase();
        }

        public bool CheckMaximumLength(string password, int maxLength)
        {
            return password.Length >= maxLength;
        }

        public bool CheckNumerial(string password)
        {
            return password.Any(char.IsDigit);
        }

        public bool CheckSpecialCharacter(string password)
        {
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        public bool CheckUppercase(string password)
        {
            return password.Any(char.IsUpper);
        }

        public async Task<List<Setting>> GetAllSettings()
        {
            return await _settingRepository.GetSettings();
        }

        public async Task<Dictionary<string, string>> GetSettingsFromRedisAsync()
        {
            try
            {
                var settings = await _RedisDb.HashGetAllAsync(SETTING_KEY);

                return settings.ToDictionary(
                    entry => entry.Name.ToString(),
                    entry => entry.Value.ToString()
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting settings from Redis: {ex.Message}");
                throw new Exception($"Error getting settings from Redis: {ex.Message}");
            }
        }

        public async Task LoadSettingsToRedisAsync()
        {
            var settingsList = await _settingRepository.GetSettings();
            var settingsDict = settingsList.ToDictionary(s => s.SettingName, s => s.Values);

            foreach (var setting in settingsDict)
            {
                await _RedisDb.HashSetAsync(SETTING_KEY, setting.Key, setting.Value);
            }
            Console.WriteLine("Saved settings into redis.");
        }

        public async Task<MessageResponseDto> UpdateSettingAsync(List<UpdateSettingRequestDto> settingRequestDtos)
        {
            try
            {
                foreach (var itemRequest in settingRequestDtos)
                {
                    var settings = await _settingRepository.GetSettings();
                    var setting = settings.FirstOrDefault(s => s.Id == itemRequest.SettingId);
                    if (setting == null) continue;

                    await _settingRepository.UpdateSetting(setting.Id, itemRequest.newValue);

                    await _RedisDb.HashSetAsync(SETTING_KEY, setting.SettingName, itemRequest.newValue);
                }

                return new MessageResponseDto
                {
                    Message = "Setting updated successfully."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating setting from repo: {ex.Message}");
                throw new Exception($"Error updating setting from repo: {ex.Message}");
            }
        }
    }
}
