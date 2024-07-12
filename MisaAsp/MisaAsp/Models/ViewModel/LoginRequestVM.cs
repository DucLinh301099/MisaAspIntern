using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class LoginRequestVM
    {
        [Required]
        public string EmailOrPhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
