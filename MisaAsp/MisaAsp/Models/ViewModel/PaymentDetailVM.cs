using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MisaAsp.Attribute.ModelAttribute;

namespace MisaAsp.Models.ViewModel
{
    public class PaymentDetailVM
    {
        [RequiredVI]
        public int Id { get; set; }

        [RequiredVI]
        [StringLengthVI(20)]
        public string DebitAccount { get; set; }

        [RequiredVI]
        [StringLengthVI(20)]
        public string CreditAccount { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        public string Amount { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        public string ObjectId { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        public string ObjectName { get; set; }

       
        public int PaymentId { get; set; }


    }
}
