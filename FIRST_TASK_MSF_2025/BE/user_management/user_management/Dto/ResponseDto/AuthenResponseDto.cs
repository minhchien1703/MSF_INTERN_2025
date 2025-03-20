namespace user_management.Dto.ResponseDto
{
    public class AuthenResponseDto
    {
        public UserResponseDto UserRes { get; set; }
        public string Token { get; set; }
    }
}
