using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel.DataAnnotations;


namespace MisaAsp.Models.DTO
{
    public class AuthResultDTO
    {
        [RequiredVI]
        public int UserId { get; set; }

        [RequiredVI]
        [StringLengthVI(256)]
        public string Token { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        public string Role { get; set; }
    }
}
