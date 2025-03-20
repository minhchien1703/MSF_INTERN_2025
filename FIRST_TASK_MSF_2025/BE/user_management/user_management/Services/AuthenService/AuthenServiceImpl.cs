using Microsoft.AspNetCore.Http.HttpResults;
using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Mappers;
using user_management.Models;
using user_management.Respositories.Authen;
using user_management.Respositories.RoleService;
using user_management.Respositories.Settings;
using user_management.Services.Settings;
using user_management.Utils;

namespace user_management.Services.AuthenService
{
    public class AuthenServiceImpl : IAuthenService
    {
        private readonly IAuthenRepository _authenReponsitory;
        private readonly IRoleRepository _roleReponsitory;
        private readonly BasicMapper _mapper;
        private readonly ISettingService _settingService;
        private readonly ISettingRepository _settingRepository;
        public AuthenServiceImpl(
            IAuthenRepository authen,
            BasicMapper mapper,
            IRoleRepository role,
            ISettingService settingService,
            ISettingRepository settingRepository)
        {
            _authenReponsitory = authen;
            _mapper = mapper;
            _roleReponsitory = role;
            _settingService = settingService;
            _settingRepository = settingRepository;
        }

        public LoginResponseDto Login(LoginRequestDto req)
        {
            try
            {
                return _authenReponsitory.login(req);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error login service: {ex.Message}");
            }
        }

        public async Task<MessageResponseDto> Register(RegisterRequestDto req)
        {
            try
            {
                var settings = await _settingService.GetSettingsFromRedisAsync();
                if (settings.TryGetValue(Constants.MAXIMUM_LENGTH, out var maxLengthStr) && int.TryParse(maxLengthStr, out int maxLength))
                {
                    if (!_settingService.CheckMaximumLength(req.Password, maxLength))
                    {
                        return new MessageResponseDto
                        {
                            Message = $"Password must have a maximum length of {maxLength} characters."
                        };
                    }
                }

                if (settings.TryGetValue(Constants.UPPER_CASE, out var uppercaseStr) && int.Parse(uppercaseStr) >= 1)
                {
                    if (!_settingService.CheckUppercase(req.Password))
                    {
                        return new MessageResponseDto
                        {
                            Message = "Password must contain at least one uppercase letter."
                        };
                    }
                }

                if (settings.TryGetValue(Constants.APECIAL_CHARACTER, out var specialCharStr) && int.Parse(specialCharStr) >= 1)
                {
                    if (!_settingService.CheckSpecialCharacter(req.Password))
                    {
                        return new MessageResponseDto
                        {
                            Message = "Password must contain at least one special character."
                        };
                    }
                }

                return await _authenReponsitory.Register(req);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine($"Error: {errorMessage}");
                throw new Exception($"Error repository: {errorMessage}", ex);
            }
        }




    }
}
