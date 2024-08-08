using MisaAsp.Models.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MisaAsp.Attribute.ModelAttribute;
using System.ComponentModel;

namespace MisaAsp.Models.ViewModel
{
    public class PaymentMasterVM
    {
        [RequiredVI]
        public int Id { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        [DisplayName("Kiểu chi tiêu")]
        public string VoucherType { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [DisplayName("Phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [StringLengthVI(100)]
        [DisplayName("Nội dung thanh toán")]
        public string BillContent { get; set; }

        [RequiredVI]
        [DisplayName("Ngày hạch toán")]
        public string AccountingDate { get; set; }

        [RequiredVI]
        [DisplayName("Ngày chứng từ")]
        public string DocumentDate { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [DisplayName("Số chứng từ")]
        public string DocumentNumber { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [DisplayName("Tổng tiền")]
        public string TotalAmount { get; set; }

        [StringLengthVI(100)]
        [DisplayName("Tên nhân viên")]
        public string EmployeeName { get; set; } 

        [StringLengthVI(50)]
        [DisplayName("Tên đối tượng")]
        public string CustomerName { get; set; }

        [StringLengthVI(50)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        
        [StringLengthVI(50)]
        [DisplayName("Tên ngân hàng")]
        public string BankName { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        [DisplayName("Số tài khoản")]
        public string AccountNumber { get; set; }

        [RequiredVI]
        [DisplayName("BankAccountId")]
        public int? BankAccountId { get; set; }

        [RequiredVI]
        public int? CustomerId { get; set; }

        [RequiredVI]    
        public int? EmployeeId { get; set; }

        [NotMapped]
        public List<PaymentDetailVM> PaymentDetails { get; set; }

    }
}
