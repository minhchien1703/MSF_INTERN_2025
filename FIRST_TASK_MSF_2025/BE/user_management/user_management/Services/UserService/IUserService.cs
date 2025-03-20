using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Models;

namespace user_management.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetUsers();
        Task<UserResponseDto> GetUserById(int id);
        Task<MessageResponseDto> UpdateUser(int userId, UpdateUserRequestDto user);
        Task<MessageResponseDto> DeleteUserById(int id);
        Task<MessageResponseDto> UpdateUserRole(int roleId, int userId);
    }
}
