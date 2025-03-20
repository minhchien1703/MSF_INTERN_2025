using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace user_management.Models
{
    [Table("BlackListToken")]
    public class BlaskListToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
