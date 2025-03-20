using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Respositories.RoleService;

namespace user_management.Services.Role
{
    public class RoleServiceImpl : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleServiceImpl(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<MessageResponseDto> DeleteRole(int roleId)
        {
            try
            {
                return await _roleRepository.DeleteRole(roleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error DELETE role by repo: {ex.Message}");
            }
        }

        public async Task<MessageResponseDto> SaveNewRole(RoleRequestDto roleRequest)
        {
            try
            {
                return await _roleRepository.SaveRole(roleRequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error SAVE new role by repo: {ex.Message}");
            }
        }
    }
}
