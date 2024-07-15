using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.BaseModel
{
    public class Services
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Location { get; set; }
        [Required]
        [StringLength(100)]
        public string Price { get; set; }
        [Required]
        [StringLength(100)]
        public string Logo { get; set; }
    }
}
