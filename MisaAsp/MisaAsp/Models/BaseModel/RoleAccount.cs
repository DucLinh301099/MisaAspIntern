
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.BaseModel
{
    public class RoleAccount
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        [StringLength(256)]
        public string RoleName { get; set; } 
    }
}
