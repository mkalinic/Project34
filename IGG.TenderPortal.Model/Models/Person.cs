using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGG.TenderPortal.Model
{
    public class Person
    {
        public int PersonId { get; set; }
        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime? BirhDate { get; set; }        
        public Employee Employee { get; set; }
    }
}
