using MisaAsp.Models.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MisaAsp.Attribute.ModelAttribute;

namespace MisaAsp.Models.ViewModel
{
    public class PaymentMasterVM
    {
        [RequiredVI]
        public int Id { get; set; }

        [RequiredVI]
        [StringLengthVI(100)]
        public string VoucherType { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        public string PaymentMethod { get; set; }

        [StringLengthVI(100)]
        public string BillContent { get; set; }

        [RequiredVI]
        public string AccountingDate { get; set; }

        [RequiredVI]
        public string DocumentDate { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        public string DocumentNumber { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        public string TotalAmount { get; set; }

        [StringLengthVI(100)]
        public string EmployeeName { get; set; } 

        [StringLengthVI(50)]
        public string ObjectName { get; set; }

        [StringLengthVI(50)]
        public string Address { get; set; }

        [RequiredVI]  
        [StringLengthVI(50)]
        public string BankName { get; set; }

        [RequiredVI]
        [StringLengthVI(50)]
        public string AccountNumber { get; set; }

        [RequiredVI]
        public int BankAccountId { get; set; }

        [RequiredVI]
        public int CustomerId { get; set; }

        [RequiredVI]
        public int EmployeeId { get; set; }

        
    }
}
