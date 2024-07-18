using MisaAsp.Attribute;

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
        public string ObjectName { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string TaxCode { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string Address { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string PhoneNumber { get; set; }
    }
}
