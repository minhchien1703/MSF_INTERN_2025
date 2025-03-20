using user_management.Dto.ResponseDto;
using user_management.Models;
using user_management.Respositories.RoleService;

namespace user_management.Mappers
{
    public class BasicMapper
    {
        private readonly IRoleRepository _roleRepository;


        public BasicMapper(IRoleRepository role)
        {
            _roleRepository = role;
        }

        public async Task<UserResponseDto> UserToUserResponse(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User is null or empty!");
            }

            UserResponseDto userResponse = new UserResponseDto();
            userResponse.Id = user.Id;
            userResponse.FirstName = user.FirstName;
            userResponse.LastName = user.LastName;
            userResponse.Email = user.Email;
            userResponse.WorkUnit = user.WorkUnit;
            userResponse.ResponsibleUnit = user.ResponsibleUnit;
            userResponse.Status = user.Status;

            var role = await _roleRepository.FindRoleById(user.Role_Id);
            if (role == null)
            {
                throw new ArgumentException("Role is null or empty!");
            }
            userResponse.Role = role;

            return userResponse;
        }

        public List<UserResponseDto> UsersToUsersResponses(List<User> users)
        {
            if (users == null || !users.Any())
            {
                throw new ArgumentException("User list is null or empty!");
            }

            List<UserResponseDto> userResponses = new List<UserResponseDto>();
            foreach (User user in users)
            {
                UserResponseDto userResponse = new UserResponseDto();
                userResponse.Id = user.Id;
                userResponse.FirstName = user.FirstName;
                userResponse.LastName = user.LastName;
                userResponse.Email = user.Email;
                userResponse.WorkUnit = user.WorkUnit;
                userResponse.ResponsibleUnit = user.ResponsibleUnit;
                userResponse.Status = user.Status;
                userResponse.Role = user.Role;

                userResponses.Add(userResponse);
            }
            return userResponses;
        }

        public List<SettingResponseDto> SettingsTosettingResponses(List<Setting> settings)
        {
            if (settings == null || !settings.Any())
            {
                throw new ArgumentException("Settings is null or empty!");
            } 

            List<SettingResponseDto> newSettingResponses = new List<SettingResponseDto>();
            foreach (Setting setting in settings)
            {
                SettingResponseDto settingResponse = new SettingResponseDto();
                settingResponse.Id = setting.Id;
                settingResponse.Description = setting.Descriptions;
                newSettingResponses.Add(settingResponse);
            }
            return newSettingResponses;
        }
    }
}
