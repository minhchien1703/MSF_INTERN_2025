﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace user_management.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string UserName{ get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string? WorkUnit { get; set; }
        [MaxLength(50)]
        public string? ResponsibleUnit { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

    }
}
