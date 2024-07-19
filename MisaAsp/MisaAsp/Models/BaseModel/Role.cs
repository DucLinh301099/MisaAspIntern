
using MisaAsp.Attribute.ModelAttribute;
namespace MisaAsp.Models.BaseModel
{
    public class Role
    {
        [RequiredVI]         
        public int Id { get; set; }
        [RequiredVI]
        [StringLengthVI(50)]
        public string RoleName { get; set; }
    }
}
