using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Attribute.ModelAttribute
{
    /// <summary>
    ///  attribute kiểm tra không được để trống thông tin
    ///  không thể null
    /// </summary>
    public class RequiredVIAttribute : RequiredAttribute
    {
        public RequiredVIAttribute() : base()
        {
            ErrorMessage = "Required_Trường {0} là bắt buộc.";
        }
    }
}
