using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;
namespace MisaAsp.Models.DTO
{
    public class AccountantGenericDTO
    {
        [RequiredVI]
        public int Id { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string Name { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string Location { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string Price { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string Logo { get; set; }
    }
}
