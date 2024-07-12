
using System.ComponentModel.DataAnnotations;
namespace MisaAsp.Models.BaseModel
{
    public class Role
    {
        [Required]         
        
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
