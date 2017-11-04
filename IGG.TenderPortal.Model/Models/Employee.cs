using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGG.TenderPortal.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(16)]
        public string EmployeeNumber { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime? EmployeeDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? Terminated { get; set; }
        [Required]
        public Person Person { get; set; }
    }
}
