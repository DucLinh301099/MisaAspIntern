using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class BankAccountVM
    {
        [RequiredVI]
        public int Id { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [BankAccountVI]
        [DisplayName("Số tài khoản")]
        public string AccountNumber { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [TextFieldVI]
        [DisplayName("Tên ngân hàng")]
        public string BankName { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [TextFieldVI]
        [DisplayName("Chi nhánh")]
        public string Branch { get; set; }

        [RequiredVI]
        public int TypeOfBank { get; set; }
    }
}
