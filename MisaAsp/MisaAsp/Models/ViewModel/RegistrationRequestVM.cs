using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class RegistrationRequestVM
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public int RoleId { get; set; } // New property to select role during registration

    }
}
