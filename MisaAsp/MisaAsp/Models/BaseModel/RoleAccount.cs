
using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.BaseModel
{
    public class RoleAccount
    {
        [RequiredVI]
        public int UserId { get; set; }
        [RequiredVI]
        public int RoleId { get; set; }
        [RequiredVI]
        [StringLengthVI(256)]
        public string RoleName { get; set; } 
    }
}
