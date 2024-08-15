namespace MisaAsp.Models.ViewModel
{
    public class FilterVM
    {
        public List<FilterConditionVM> Conditions { get; set; }
        public string LogicalOperator { get; set; } // Ví dụ: "and", "or"
    }
}
