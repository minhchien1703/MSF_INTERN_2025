using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user_management.Dto.RequestDto;
using user_management.Services.Role;

namespace user_management.Controllers
{
    [Route("api/v1/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService; 
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole(RoleRequestDto roleRequest)
        {
            try
            {
                return Ok(await _roleService.SaveNewRole(roleRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error insert role: {ex.Message}");
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-role/{roleId}")]
        public async Task<IActionResult> deleteRole(int roleId)
        {
            try
            {
                return Ok(await _roleService.DeleteRole(roleId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error delete role: {ex.Message}");
            }
        }

    }
}
