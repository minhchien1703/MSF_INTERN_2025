using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace user_management.Models
{
    [Table("apiLog")]
    public class ApiLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Method { get; set; } 
        [MaxLength(50)]
        public string userName { get; set; }
        [MaxLength(50)]
        public string Endpoint { get; set; } 
        [MaxLength(255)]
        public int StatusCode { get; set; } 
        [MaxLength(45)]
        public string? IpAddress { get; set; } 
        [MaxLength(50)]
        public string TimeLimit { get; set; } 
        public DateTime Timestamp { get; set; }
    }
}
