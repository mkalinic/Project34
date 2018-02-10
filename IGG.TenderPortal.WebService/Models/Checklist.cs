
namespace IGG.TenderPortal.WebService.Models
{
    public class Checklist
    {

        public int ID { get; set; } = -1;
        public int projectID { get; set; }
        public string item { get; set; }
        public int itemOrder { get; set; }
        public bool Checked { get; set; }
 
    }
}