using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MisaAsp.Attribute.ModelAttribute;

namespace MisaAsp.Models.BaseModel
{
    [Table("customer", Schema = "public")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [RequiredVI]
        [Column("objectid")]
        [StringLengthVI(50)]
        public string ObjectId { get; set; }

        [RequiredVI]
        [Column("objectname")]
        [StringLengthVI(100)]
        public string ObjectName { get; set; }

        [RequiredVI]
        [Column("taxcode")]
        [StringLengthVI(20)]
        public string TaxCode { get; set; }

        [RequiredVI]
        [Column("address")]
        [StringLengthVI(200)]
        public string Address { get; set; }

        [RequiredVI]
        [Column("phonenumber")]
        [StringLengthVI(15)]
        public string PhoneNumber { get; set; }
    }
}
