using Microsoft.AspNetCore.Http.HttpResults;
using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Mappers;
using user_management.Models;
using user_management.Respositories.UserRepository;

namespace user_management.Services.UserService
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly BasicMapper _mapper;
        public UserServiceImpl(IUserRepository userRepository, BasicMapper basicMapper)
        {
            _userRepository = userRepository;
            _mapper = basicMapper;
        }
        public async Task<MessageResponseDto> DeleteUserById(int id)
        {
            return await _userRepository.DeleteUserById(id);
        }

        public async Task<UserResponseDto> GetUserById(int id)
        {
            return await _mapper.UserToUserResponse(await _userRepository.GetUserById(id));
        }

        public async Task<List<UserResponseDto>> GetUsers()
        {
            return _mapper.UsersToUsersResponses(await _userRepository.GetUsers());
        }

        public async Task<MessageResponseDto> UpdateUser(int userId, UpdateUserRequestDto user)
        {
            return await _userRepository.UpdateUserById(userId, user);
        }

        public async Task<MessageResponseDto> UpdateUserRole(int roleId, int userId)
        {
            return await _userRepository.UpdateUserRole(roleId, userId);
        }

        
    }
}
