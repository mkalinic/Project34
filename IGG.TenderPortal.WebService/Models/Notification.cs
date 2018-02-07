using System;

namespace IGG.TenderPortal.WebService.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public int IDproject { get; set; }
        public string message { get; set; }
        public DateTime time { get; set; }
    }
}
