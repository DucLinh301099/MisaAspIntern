using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class RegistrationRequestVM
    {
        [RequiredVI]
        [StringLengthVI(100)]
        public string FirstName { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        public string LastName { get; set; }

        [RequiredVI(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredVI]
        [Phone]
        public string PhoneNumber { get; set; }

        [RequiredVI]
        [MinLength(6)]
        public string Password { get; set; }
        public int RoleId { get; set; } // New property to select role during registration

    }
}
