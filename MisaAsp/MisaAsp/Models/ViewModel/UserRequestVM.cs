using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class UserRequestVM
    {
        [RequiredVI]
        public int Id { get; set; }

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
        [StringLengthVI(100)]
        public string Email { get; set; }

        [RequiredVI]
        [Phone]
        [StringLengthVI(100)]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        
        [RequiredVI]
        public int RoleId { get; set; }

        [RequiredVI]
        public string RoleName { get; set; }
    }
}
