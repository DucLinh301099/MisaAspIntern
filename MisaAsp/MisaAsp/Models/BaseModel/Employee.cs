using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MisaAsp.Attribute.ModelAttribute;

namespace MisaAsp.Models.BaseModel
{
    [Table("employee", Schema = "public")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [RequiredVI]
        [Column("employeecode")]
        [StringLengthVI(50)]
        public string EmployeeCode { get; set; }

        [RequiredVI]
        [Column("employeename")]
        [StringLengthVI(100)]
        public string EmployeeName { get; set; }

        [RequiredVI]
        [Column("department")]
        [StringLengthVI(100)]
        public string Department { get; set; }

        [Column("mobilephone")]
        [StringLengthVI(15)]
        public string MobilePhone { get; set; }
    }
}
