using IGG.TenderPortal.Common;
using System;
using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class Milestone
    {
        public int MilestoneId { get; set; }
        public Tender Tender { get; set; }
        public string Name { get; set; }
        public DateTime WillBeAt { get; set; }
        public bool IsDone { get; set; }
        public ClientType NotificationTo { get; set; }
        public DateTime? NotificationDate { get; set; }
        public ClientType VisibleFor { get; set; }
        public virtual IList<TenderNotification> TenderNotifications { get; set; }
    }
}
