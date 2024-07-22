using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MisaAsp.Attribute.ModelAttribute
{
    /// <summary>
    /// Attribute kiểm tra format nhập liệu  của cả email và sđt
    /// </summary>
    public class EmailOrPhoneVIAttribute : ValidationAttribute
    {
        // Constructor thiết lập thông báo lỗi mặc định
        public EmailOrPhoneVIAttribute() : base()
        {
            ErrorMessage = "InvalidFormat_Trường {0} phải là định dạng hợp lệ";
        }

        // Phương thức kiểm tra giá trị đầu vào có hợp lệ hay không
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            string input = value.ToString();
            // Kiểm tra xem giá trị đầu vào có phải là số điện thoại hoặc email hợp lệ không
            if (IsValidPhoneNumber(input) || IsValidEmail(input))
            {
                return ValidationResult.Success;
            }

            // Nếu không hợp lệ, trả về kết quả không hợp lệ với thông báo lỗi
            return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
        }

        // Phương thức kiểm tra tính hợp lệ của số điện thoại
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^\d{9,13}$");
            return regex.IsMatch(phoneNumber);
        }

        // Phương thức kiểm tra tính hợp lệ của email
        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }
    }
}
