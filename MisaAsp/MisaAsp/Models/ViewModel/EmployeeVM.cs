using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;
namespace MisaAsp.Models.ViewModel
{
    public class EmployeeVM
    {
        [RequiredVI]
        public int Id { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string EmployeeCode { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        [TextFieldVI]
        public string EmployeeName { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        [TextFieldVI]
        public string Department { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        [PhoneVI]
        public string MobilePhone { get; set; }
    }
}
