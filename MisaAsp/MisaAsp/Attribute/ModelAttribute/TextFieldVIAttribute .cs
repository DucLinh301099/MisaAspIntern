using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MisaAsp.Attribute.ModelAttribute
{
    /// <summary>
    /// Attribute kiểm tra format của các trường văn bản như tên người dùng hoặc tên ngân hàng
    /// </summary>
    public class TextFieldVIAttribute : ValidationAttribute
    {
        public TextFieldVIAttribute() : base()
        {
            ErrorMessage = "InvalidFormat_Trường {0} chỉ được chứa các ký tự chữ cái, khoảng trắng và dấu gạch ngang";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            string textField = value.ToString();
            if (!IsValidTextField(textField))
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        private bool IsValidTextField(string textField)
        {
            // regex yêu cầu tên chỉ chứa các ký tự chữ cái, khoảng trắng và dấu gạch ngang (dấu gạch ngang là tùy chọn)
            var regex = new Regex(@"^[a-zA-Z\s-]+$");
            return regex.IsMatch(textField);
        }
    }
}
