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

        [Required]
        [Column("account_number")]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [Column("bank_name")]
        [StringLength(50)]
        public string BankName { get; set; }

        [Required]
        [Column("branch")]
        [StringLength(50)]
        public string Branch { get; set; }

        [Required]
        [Column("typeofbank")]
        public int TypeOfBank { get; set; }
    }
}

