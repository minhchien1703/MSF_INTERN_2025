using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_management.Data;
using user_management.Dto.RequestDto;
using user_management.Dto.ResponseDto;
using user_management.Models;
using user_management.Respositories.UserRepository;
using user_management.Services.UserService;

namespace user_management.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // API GET USERS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        { 
            try
            {
                return Ok(await _userService.GetUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fetching users error: {ex.Message}");
            }
        }

        //API FIND USER BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                return Ok(await _userService.GetUserById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Find user error: {ex.Message}");
            }
        }

        //API UPDATE USER
        [Authorize]
        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> EditUser(int id, UpdateUserRequestDto user)
        {
            try
            {
                return Ok(await _userService.UpdateUser(id, user));
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Update user error: {ex.Message}");
            }
        }

        //API UPDATE USER
        [Authorize]
        [HttpPut("update-myself")]
        public async Task<IActionResult> UpdateMyself(UpdateUserRequestDto user)
        {
            try
            {
                var id = int.Parse(HttpContext.Session.GetString("UserId"));
                return Ok(await _userService.UpdateUser(id, user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Update user error: {ex.Message}");
            }
        }

        //API UPDATE USER ROLE
        [Authorize(Roles = "ADMIN")]
        [HttpPut("update-role")]
        public async Task<IActionResult> EditUserRole(UpdateUserRoleRequestDto requestDto)
        {
            try
            { 
                var userId = requestDto.UserId;
                var roleId = requestDto.RoleId;
                return Ok(await _userService.UpdateUserRole(roleId, userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Update error: {ex.Message}");
            }
        }

        //API DELETE USER
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return Ok(await _userService.DeleteUserById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Delete user error: {ex.Message}");
            }
        }


        //Get profile User
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            string userId = HttpContext.Session.GetString("UserId");
            string userEmail = HttpContext.Session.GetString("UserEmail");
            string userRole = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not logged in.");
            }
            var ut = new { Id = userId, Email = userEmail, Role = userRole };
            Console.WriteLine($"DI THEC: {ut}");

            return Ok(new
            {
                Id = userId,
                Email = userEmail,
                Role = userRole
            });
        }


    }
}
