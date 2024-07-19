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
        public string EmployeeName { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string Department { get; set; }
        [RequiredVI]
        [StringLengthVI(100)]
        public string MobilePhone { get; set; }
    }
}
