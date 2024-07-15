using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisaAsp.Models.BaseModel
{
    [Table("customer", Schema = "public")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("objectid")]
        [StringLength(50)]
        public string ObjectId { get; set; }

        [Required]
        [Column("objectname")]
        [StringLength(100)]
        public string ObjectName { get; set; }

        [Required]
        [Column("taxcode")]
        [StringLength(20)]
        public string TaxCode { get; set; }

        [Required]
        [Column("address")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [Column("phonenumber")]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
    }
}
