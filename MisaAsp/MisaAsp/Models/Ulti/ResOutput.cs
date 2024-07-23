using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MisaAsp.Models.Ulti
{
    public class ResOutput
    {
        public string message { get; set; }
        public object data { get; set; }
        public object code { get; set; }
        public bool isSuccess { get; set; }
        public ResOutput() { }
        public void HandleSuccess(string message, object data = null)
        {
            this.message = message;
            this.data = data;
            isSuccess = true;
        }

        public void HandleError(string message = "Thất bại", object data = null, object code = null)
        {
            this.message = message;
            this.data = data;
            isSuccess = false;
            this.code = code;
        }
    }

    public class ColumnError()
    {
        public string FieldName { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }
}
