using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MisaAsp.Attribute.ModelAttribute
{
    public class PhoneVIAttribute : ValidationAttribute
    {
        public PhoneVIAttribute() : base()
        {
            ErrorMessage = "InvalidFormat_Trường {0} phải là định dạng hợp lệ";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            string phoneNumber = value.ToString();
            if (!IsValidPhoneNumber(phoneNumber))
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^\d{9,13}$");
            return regex.IsMatch(phoneNumber);
        }
    }
}
