namespace MisaAsp.Models.Ulti
{
    public class PagingResult
    {
        public List<object> PageData { get; set; }
        //public SummaryData SummaryData { get; set; }
        public int Total { get; set; }
        public string TotalAmount { get; set; }
        //public int TotalDisplay { get; set; }
        //public bool TableEmpty { get; set; }
    }

    public class SummaryData
    {
        public int Total { get; set; }
        public int TotalAmount { get; set; }
    }
}
