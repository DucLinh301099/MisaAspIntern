using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisaAsp.Models.BaseModel
{
    [Table("bank_account", Schema = "public")]
    public class BankAccount
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [RequiredVI]
        [Column("account_number")]
        [StringLengthVI(50)]
        public string AccountNumber { get; set; }

        [RequiredVI]
        [Column("bank_name")]
        [StringLengthVI(50)]
        public string BankName { get; set; }

        [RequiredVI]
        [Column("branch")]
        [StringLengthVI(50)]
        public string Branch { get; set; }

        [RequiredVI]
        [Column("typeofbank")]
        public int TypeOfBank { get; set; }
    }
}

