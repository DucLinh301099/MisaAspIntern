using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;
namespace MisaAsp.Models.ViewModel
{
    public class EmployeeVM
    {
        [RequiredVI]
        public int Id { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [DisplayName("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [DisplayName("Tên nhân viên")]

        public string EmployeeName { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [DisplayName("Đơn vị")]

        public string Department { get; set; }

        [RequiredVI]
        [DisplayName("Số điện thoại")]
        [StringLengthVI(100)]
        [PhoneVI]
        public string PhoneNumber { get; set; }
    }
}
