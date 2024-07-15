using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class LoginRequestVM
    {
        [Required]
        [StringLength(100)]
        public string EmailOrPhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
