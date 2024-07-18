using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class BankAccountVM
    {
        [RequiredVI]
        public int Id { get; set; }
        [RequiredVI]
        [StringLengthVI(50)]
        public string AccountNumber { get; set; }
        [RequiredVI]
        [StringLengthVI(50)]
        public string BankName { get; set; }
        [RequiredVI]
        [StringLengthVI(50)]
        public string Branch { get; set; }
        [RequiredVI]
        public int TypeOfBank { get; set; }
    }
}
