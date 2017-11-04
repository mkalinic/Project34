using System;

namespace IGG.TenderPortal.Model
{
    public class TenderNotification
    {
        public int TenderNotificationId { get; set; }
        public Tender Tender { get; set; }
        public string Message { get; set; }
        public DateTime SendAt { get; set; }
    }
}
