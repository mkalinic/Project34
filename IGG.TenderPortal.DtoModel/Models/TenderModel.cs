using System.ComponentModel.DataAnnotations;

namespace IGG.TenderPortal.DtoModel
{
    public class TenderModel
    {
        public int TenderId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public bool IsClosed { get; set; }
    }
}
