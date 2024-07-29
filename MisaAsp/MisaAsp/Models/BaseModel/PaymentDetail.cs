using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;

namespace MisaAsp.Models.BaseModel
{
    [Table("paymentdetail", Schema = "public")]
    public class PaymentDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("debit_account")]
        [StringLengthVI(20)]
        public string DebitAccount { get; set; }

        [Column("credit_account")]
        [StringLengthVI(20)]
        public string CreditAccount { get; set; }

        [Column("amount")]
       
        public decimal? Amount { get; set; }

        [Column("objectid")]
        [StringLengthVI(50)]
        public string ObjectId { get; set; }

        [RequiredVI]
        [Column("payment_id")]
        public int PaymentId { get; set; }

        [Column("objectname")]
        [StringLengthVI(100)]
        public string ObjectName { get; set; }
    }
}
