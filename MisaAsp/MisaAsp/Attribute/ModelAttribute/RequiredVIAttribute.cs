using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Attribute.ModelAttribute
{
    public class RequiredVIAttribute : RequiredAttribute
    {
        public RequiredVIAttribute() : base()
        {
            ErrorMessage = "Required_Trường {0} là bắt buộc.";
        }
    }
}
