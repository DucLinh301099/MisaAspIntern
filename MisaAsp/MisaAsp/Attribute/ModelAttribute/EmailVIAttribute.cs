using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MisaAsp.Attribute.ModelAttribute
{
    public class EmailVIAttribute : ValidationAttribute
    {
        public EmailVIAttribute() : base()
        {
            ErrorMessage = "InvalidFormat_Trường {0} phải là định dạng hợp lệ";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            string email = value.ToString();
            if (!IsValidEmail(email))
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }
    }
}
