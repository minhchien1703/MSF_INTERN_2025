using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace user_management.Models
{
    [Table("Settings")]
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        public string SettingName{ get; set; }
        [MaxLength(50)]
        public string Values { get; set; }
        [MaxLength(255)]
        public string Descriptions { get; set; }
    }
}
