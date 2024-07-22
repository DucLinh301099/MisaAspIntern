using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Attribute.ModelAttribute
{
    /// <summary>
    ///  attribute kiểm tra format của input không vượt quá ký tự cho phép
    /// </summary>
    public class StringLengthVIAttribute : StringLengthAttribute
    {
        public StringLengthVIAttribute(int length) : base(length)
        {
            ErrorMessage = $"StringLength_Trường {{0}} phải là chuỗi có độ dài tối đa {length} ký tự.";
        }
    }
}
