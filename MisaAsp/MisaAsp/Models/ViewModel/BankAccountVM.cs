using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class BankAccountVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string BankName { get; set; }
        [Required]
        [StringLength(50)]
        public string Branch { get; set; }
        [Required]
        public int TypeOfBank { get; set; }
    }
}
