using System;
using System.ComponentModel.DataAnnotations;

namespace IGG.TenderPortal.DtoModel.Models
{
    public class MessageModel
    {
        public int MessageId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]        
        public int UserId { get; set; }
        [Required]
        public int TenderId { get; set; }
    }
}
