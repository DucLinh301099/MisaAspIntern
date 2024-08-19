
using MisaAsp.Models.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;
using MisaAsp.Models.ViewModel;

namespace MisaAsp.Models.Ulti
{
    public class PagingFilter
    {
        public List<object> Filters { get; set; }

        public List<SortVM> Sort { get; set; }
        [Required]
        public int CurrentPage { get; set; }
        [Required]
        public int ItemsPerPage { get; set; }
        public string View { get; set; }
    
    }

    public class Filter
    {
        public Filter(List<object> filters, int paramIndex = 0)
        {
            Filters = filters;
            ParamIndex = paramIndex;
        }
        public List<object> Filters { get; set; }

        public int ParamIndex { get; set; } = 0;
    }
}
