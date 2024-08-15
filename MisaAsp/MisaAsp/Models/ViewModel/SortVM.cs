using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel

{
    public class SortVM
    {
        [Required]
        public string Property { get; set; }
        [Required]
        public bool Desc { get; set; }
    }
}
