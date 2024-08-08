using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MisaAsp.Attribute;
using MisaAsp.Attribute.ModelAttribute;

namespace MisaAsp.Models.BaseModel
{
    [Table("paymentmaster", Schema = "public")]
    public class PaymentMaster
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [RequiredVI]
        [Column("vouchertype")]
        [StringLengthVI(100)]
        public string VoucherType { get; set; }

        [RequiredVI]
        [Column("paymentmethod")]
        [StringLengthVI(50)]
        public string PaymentMethod { get; set; }

        [Column("billcontent")]
        [StringLengthVI(100)]
        public string BillContent { get; set; }

        [RequiredVI]
        [Column("accounting_date")]
        public DateTime AccountingDate { get; set; }

        [RequiredVI]
        [Column("document_date")]
        public DateTime DocumentDate { get; set; }

        [RequiredVI]
        [Column("document_number")]
        [StringLengthVI(50)]
        public string DocumentNumber { get; set; }

        [RequiredVI]
        [Column("totalamount")]
        
        public decimal TotalAmount { get; set; }

        [Column("employeename")]
        [StringLengthVI(100)]
        public string EmployeeName { get; set; }

        [Column("objectname")]
        [StringLengthVI(50)]
        public string CustomerName { get; set; }

        [Column("address")]
        [StringLengthVI(50)]
        public string Address { get; set; }

        [RequiredVI]
        [Column("bankname")]
        [StringLengthVI(50)]
        public string BankName { get; set; }

        [RequiredVI]
        [Column("accountnumber")]
        [StringLengthVI(50)]
        public string AccountNumber { get; set; }

        [RequiredVI]
        [Column("bank_account_id")]
        public int BankAccountId { get; set; }

        [RequiredVI]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [RequiredVI]
        [Column("employee_id")]
        public int EmployeeId { get; set; }
    }
}
