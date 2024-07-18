using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Attribute
{
    public class StringLengthVIAttribute : StringLengthAttribute
    {
        public StringLengthVIAttribute(int length) : base(length)
        {
            ErrorMessage = $"StringLength_Trường {{0}} phải là chuỗi có độ dài tối đa {length} ký tự.";
        }

        public string ErrorCode => "StringLength";
    }
}
