using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MisaAsp.Models.Ulti
{
    public class ResOutput
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public object Code { get; set; }
        public bool IsSuccess { get; set; }
        public ResOutput() { }
        public void HandleSuccess(string message, object data = null)
        {
            Message = message;
            Data = data;
            IsSuccess = true;
        }

        public void HandleError(string message = "Thất bại", object data = null, object code = null)
        {
            Message = message;
            Data = data;
            IsSuccess = false;
            Code = code;
        }
    }

    public class ColumnError()
    {
        public string FieldName { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }
}
