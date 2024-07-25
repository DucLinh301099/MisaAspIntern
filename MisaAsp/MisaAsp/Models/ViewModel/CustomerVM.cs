using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;

namespace MisaAsp.Models.ViewModel
{
    public class CustomerVM
    {
        [RequiredVI]
        public int Id { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string ObjectId { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        [TextFieldVI]
        public string ObjectName { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string TaxCode { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        [TextFieldVI]
        public string Address { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        [PhoneVI]
        public string PhoneNumber { get; set; }
    }
}
