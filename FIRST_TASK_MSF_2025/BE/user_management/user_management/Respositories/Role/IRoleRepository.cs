using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Models;

namespace user_management.Respositories.RoleService
{
    public interface IRoleRepository
    {
        Task<Role> FindRoleById(int id);
        Task<MessageResponseDto> SaveRole(RoleRequestDto roleRequest);
        Task<MessageResponseDto> DeleteRole(int roleId);
    }
}
