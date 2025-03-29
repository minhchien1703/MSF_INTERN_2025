using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace user_management.Models
{
    [Table("ApiLog")]
    public class ApiLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FuncName { get; set; }
        [MaxLength(10)]
        public string? IpAddress { get; set; } 
        public DateTime Timestamp { get; set; }
        [MaxLength(50)]
        public string TimeLimit { get; set; }
        [MaxLength(100)]
        public string BrowserInfo { get; set; }
    }
}
