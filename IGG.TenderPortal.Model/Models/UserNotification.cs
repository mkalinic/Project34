using System;

namespace IGG.TenderPortal.Model
{
    public class UserNotification
    {
        public int UserNotificationId { get; set; }
        public Milestone Milestone { get; set; }
        public string Message { get; set; }
        public DateTime SendAt { get; set; }
    }
}
