using System.ComponentModel.DataAnnotations;


namespace MisaAsp.Models.BaseModel
{
    public class AuthResult
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(256)]
        public string Token { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }
    }
}
