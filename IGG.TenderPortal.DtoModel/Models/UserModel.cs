using System.ComponentModel.DataAnnotations;

namespace IGG.TenderPortal.DtoModel
{
    public class UserModel
    {

        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
