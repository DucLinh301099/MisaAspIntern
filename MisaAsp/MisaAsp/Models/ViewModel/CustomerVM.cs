using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class CustomerVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ObjectId { get; set; }
        [Required]
        public string ObjectName { get; set; }
        [Required]
        public string TaxCode { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
