using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisaAsp.Models.BaseModel
{
    [Table("services", Schema = "public")]
    public class Services
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Column("location")]
        [StringLength(255)]
        public string Location { get; set; }

        [Column("price")]
        [StringLength(255)]
        public string Price { get; set; }

        [Column("logo")]
        public string Logo { get; set; }
    }
}
