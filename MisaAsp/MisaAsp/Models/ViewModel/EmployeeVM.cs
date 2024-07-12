using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class EmployeeVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string MobilePhone { get; set; }
    }
}
