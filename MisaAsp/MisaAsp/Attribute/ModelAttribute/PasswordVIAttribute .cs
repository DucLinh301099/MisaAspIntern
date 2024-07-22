using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MisaAsp.Attribute.ModelAttribute
{
    /// <summary>
    ///  attribute kiểm tra format của password
    /// </summary>
    public class PasswordVIAttribute : ValidationAttribute
    {
        public PasswordVIAttribute() : base()
        {
            ErrorMessage = "InvalidFormat_Trường {0} phải chứa chữ cái đầu viết hoa, có cả chữ số và chữ thường";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            string password = value.ToString();
            if (!IsValidPassword(password))
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        private bool IsValidPassword(string password)
        {
         
            var regex = new Regex(@"^[A-Z][a-zA-Z\d]*\d[a-zA-Z\d]*$");
            return regex.IsMatch(password);
        }
    }
}
