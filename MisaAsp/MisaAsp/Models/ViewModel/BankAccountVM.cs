using MisaAsp.Attribute.ModelAttribute;
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
        public string AccountNumber { get; set; }
        [RequiredVI]
        [StringLengthVI(50)]
        [TextFieldVI]
        public string BankName { get; set; }
        [RequiredVI]
        [StringLengthVI(50)]
        [TextFieldVI]
        public string Branch { get; set; }
        [RequiredVI]
        public int TypeOfBank { get; set; }
    }
}
