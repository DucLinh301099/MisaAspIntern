using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MisaAsp.Attribute.ModelAttribute
{
    /// <summary>
    /// Attribute kiểm tra format của số tài khoản ngân hàng
    /// </summary>
    public class BankAccountVIAttribute : ValidationAttribute
    {
        public BankAccountVIAttribute() : base()
        {
            ErrorMessage = "InvalidFormat_Trường {0} phải là số tài khoản ngân hàng hợp lệ";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            string bankAccount = value.ToString();
            if (!IsValidBankAccount(bankAccount))
            {
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        private bool IsValidBankAccount(string bankAccount)
        {
            // regex yêu cầu số tài khoản ngân hàng phải là số và có độ dài từ 8 đến 16 ký tự
            var regex = new Regex(@"^\d{8,16}$");
            return regex.IsMatch(bankAccount);
        }
    }
}
