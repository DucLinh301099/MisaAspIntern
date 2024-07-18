using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Attribute
{
    public class RequiredVIAttribute : RequiredAttribute
    {
        public RequiredVIAttribute() : base()
        {
            ErrorMessage = $"Required_Trường {{0}} không được để trống.";
        }

        public string ErrorCode => "Required";
    }
}
