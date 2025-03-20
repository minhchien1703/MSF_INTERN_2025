using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;

namespace user_management.Services.Role
{
    public interface IRoleService
    {
        Task<MessageResponseDto> SaveNewRole(RoleRequestDto roleRequest);
        Task<MessageResponseDto> DeleteRole(int roleId);
    }
}
