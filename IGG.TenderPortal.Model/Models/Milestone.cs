using System;
using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class Milestone
    {
        public int MilestoneId { get; set; }
        public Tender Tender { get; set; }
        public MilestoneType MilestoneType { get; set; }
        public DateTime WillBeAt { get; set; }
        public bool IsDone { get; set; }
        public virtual IList<TenderNotification> TenderNotifications { get; set; }
    }
}
