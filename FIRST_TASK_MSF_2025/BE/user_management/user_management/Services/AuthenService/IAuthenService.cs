using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;

namespace user_management.Services.AuthenService
{
    public interface IAuthenService
    {
        Task<MessageResponseDto> Register(RegisterRequestDto req);
        LoginResponseDto Login(LoginRequestDto req);
    }
}
