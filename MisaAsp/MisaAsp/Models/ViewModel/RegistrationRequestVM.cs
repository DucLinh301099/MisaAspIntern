using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class RegistrationRequestVM
    {
        [RequiredVI]
        [StringLengthVI(100)]
        [DisplayName("Họ và đệm")]
        public string FirstName { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [DisplayName("Tên")]
        public string LastName { get; set; }

        [RequiredVI]
        [EmailVI]
      
        public string Email { get; set; }

        [RequiredVI]
        [PhoneVI]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [PasswordVI]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [RequiredVI]
        public int RoleId { get; set; } 

    }
}
