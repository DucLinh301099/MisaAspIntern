﻿using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class CustomerVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ObjectId { get; set; }
        [Required]
        [StringLength(100)]
        public string ObjectName { get; set; }
        [Required]
        [StringLength(100)]
        public string TaxCode { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; }
    }
}
