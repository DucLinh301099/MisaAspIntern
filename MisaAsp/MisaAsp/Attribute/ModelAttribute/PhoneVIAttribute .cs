using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MisaAsp.Attribute.ModelAttribute
{
    /// <summary>
    ///  attribute kiểm tra format của sđt
    /// </summary>
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
            // regex yêu cầu sđt ít nhất 9 và nhiều nhất 13 chữ số
            var regex = new Regex(@"^\d{9,13}$");
            return regex.IsMatch(phoneNumber);
        }
    }
}
