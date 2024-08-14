
using MisaAsp.Models.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;
namespace MisaAsp.Models.ViewModel
{
    public class PagingFilterVM
    {
        public int Id { get; set; }
        
        public string Filters { get; set; }
         
        public string Sort { get; set; }
        [Required] 
        public int CurrentPage { get; set; }
        [Required] 
        public int ItemsPerPage { get; set; }
        [Required] 
        public int TotalPages { get; set; }
        [Required] 
        public int totalRecords { get; set; } 

    }
}
