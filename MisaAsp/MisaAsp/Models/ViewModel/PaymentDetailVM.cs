using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;

namespace MisaAsp.Models.ViewModel
{
    public class PaymentDetailVM
    {
        [RequiredVI]
        public int DetailsId { get; set; }

        [RequiredVI]
        [StringLengthVI(20)]
        [DisplayName("Tài khoản nợ")]
        public string DebitAccount { get; set; }

        [RequiredVI]
        [StringLengthVI(20)]
        [DisplayName("Tài khoản có")]
        public string CreditAccount { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [DisplayName("Số tiền")]
        public string Amount { get; set; }

        [RequiredVI]
        [DisplayName("Mã đối tượng")]
        [StringLengthVI(50)]
        public string ObjectId { get; set; }

        [RequiredVI]
        [DisplayName("Tên đối tượng")]
        [StringLengthVI(100)]
        public string ObjectName { get; set; }

       
        public int PaymentId { get; set; }


    }
}
