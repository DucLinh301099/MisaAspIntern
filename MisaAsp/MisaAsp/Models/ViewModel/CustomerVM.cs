using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;

namespace MisaAsp.Models.ViewModel
{
    public class CustomerVM
    {
        [RequiredVI]
        public int Id { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [DisplayName("Mã khách hàng")]
        public string ObjectId { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [TextFieldVI]
        [DisplayName("Tên khách hàng")]
        public string ObjectName { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [DisplayName("Mã số thuế")]
        public string TaxCode { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [TextFieldVI]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [PhoneVI]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
    }
}
