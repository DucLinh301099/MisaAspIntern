using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.BaseModel
{
    public class Users
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

    
        [Required]
        public int RoleId { get; set; }

        
    }
}
