using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisaAsp.Models.BaseModel
{
    [Table("users", Schema = "public")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("firstname")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [Column("lastname")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [Column("email")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("phonenumber")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [Column("roleid")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
