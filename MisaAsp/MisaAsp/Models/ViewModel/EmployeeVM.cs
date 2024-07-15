﻿using System.ComponentModel.DataAnnotations;

namespace MisaAsp.Models.ViewModel
{
    public class EmployeeVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [Required]
        [StringLength(100)]
        public string Department { get; set; }
        [Required]
        [StringLength(100)]
        public string MobilePhone { get; set; }
    }
}
