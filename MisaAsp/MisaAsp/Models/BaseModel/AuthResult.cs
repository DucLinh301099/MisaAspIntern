using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;


namespace MisaAsp.Models.BaseModel
{
    public class AuthResult
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
