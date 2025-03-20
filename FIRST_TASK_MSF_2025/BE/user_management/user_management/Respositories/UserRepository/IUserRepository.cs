using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Models;

namespace user_management.Respositories.UserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<MessageResponseDto> UpdateUserById(int id, UpdateUserRequestDto userRequest);
        Task<MessageResponseDto> DeleteUserById(int id);
        Task<MessageResponseDto> UpdateUserRole(int roleId, int userId);
    }
}
