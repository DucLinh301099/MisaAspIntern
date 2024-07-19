using MisaAsp.Attribute.ModelAttribute;
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

        [RequiredVI]
        [EmailVI]
        public string Email { get; set; }

        [RequiredVI]
        [PhoneVI]
        public string PhoneNumber { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [PasswordVI]
        public string Password { get; set; }
        public int RoleId { get; set; } 

    }
}
