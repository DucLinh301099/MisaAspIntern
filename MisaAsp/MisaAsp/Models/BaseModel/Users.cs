using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MisaAsp.Attribute.ModelAttribute;
namespace MisaAsp.Models.BaseModel
{
    [Table("users", Schema = "public")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [RequiredVI]
        [Column("firstname")]
        [StringLengthVI(255)]
        public string FirstName { get; set; }

        [RequiredVI]
        [Column("lastname")]
        [StringLengthVI(255)]
        public string LastName { get; set; }

        [RequiredVI]
        [Column("email")]
        [StringLengthVI(255)]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredVI]
        [Column("phonenumber")]
        [StringLengthVI(20)]
        public string PhoneNumber { get; set; }

        [RequiredVI]
        [Column("password")]
        [StringLengthVI(255)]
        public string Password { get; set; }

        [RequiredVI]
        [Column("roleid")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
