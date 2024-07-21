using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class LoginRequestVM
    {
        [RequiredVI]
        [StringLengthVI(50)]
        [DisplayName("Số điện thoại hoặc email")]
        [EmailOrPhoneVI]

        public string EmailOrPhoneNumber { get; set; }

        [RequiredVI]
        [StringLengthVI(15)]
        public string Password { get; set; }
    }
}
