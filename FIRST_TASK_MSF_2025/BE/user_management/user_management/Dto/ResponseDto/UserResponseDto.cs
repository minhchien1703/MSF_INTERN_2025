using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using user_management.Models;

namespace user_management.Dto.ResponseDto
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WorkUnit { get; set; }
        public string ResponsibleUnit { get; set; }
        public Boolean Status { get; set; }
        public Role Role { get; set; }
    }
}
