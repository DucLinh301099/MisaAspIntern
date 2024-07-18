using MisaAsp.Attribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class LoginRequestVM
    {
        [RequiredVI]
        [StringLengthVI(15)]
        [DisplayName("Số điện thoại hoặc email")]
        public string EmailOrPhoneNumber { get; set; }

        [RequiredVI]
        [StringLengthVI(15)]
        public string Password { get; set; }
    }
}
