using System.ComponentModel.DataAnnotations;

namespace IGG.TenderPortal.DtoModel
{
    public class UserTenderModel
    {
        public int UserTenderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TenderId { get; set; }        
    }
}
