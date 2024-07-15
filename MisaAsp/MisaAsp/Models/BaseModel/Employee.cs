using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisaAsp.Models.BaseModel
{
    [Table("employee", Schema = "public")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("employeecode")]
        [StringLength(50)]
        public string EmployeeCode { get; set; }

        [Required]
        [Column("employeename")]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        [Column("department")]
        [StringLength(100)]
        public string Department { get; set; }

        [Column("mobilephone")]
        [StringLength(15)]
        public string MobilePhone { get; set; }
    }
}
