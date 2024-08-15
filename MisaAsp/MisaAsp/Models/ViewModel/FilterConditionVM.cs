namespace MisaAsp.Models.ViewModel
{
    public class FilterConditionVM
    {
        public string Field { get; set; }
        public string Operator { get; set; } // Ví dụ: ">=", "<=", "=", etc.
        public string Value { get; set; }
    }
}
