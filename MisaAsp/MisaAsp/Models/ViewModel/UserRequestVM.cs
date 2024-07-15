using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class UserRequestVM
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

        // New properties for RoleId and RoleName
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
