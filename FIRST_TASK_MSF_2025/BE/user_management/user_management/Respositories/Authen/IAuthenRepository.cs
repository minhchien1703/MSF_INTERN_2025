using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Models;

namespace user_management.Respositories.Authen
{
    public interface IAuthenRepository
    {
        Task<MessageResponseDto> Register(RegisterRequestDto req);
        LoginResponseDto login(LoginRequestDto req);
    }
}
