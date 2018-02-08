using IGG.TenderPortal.Common;
using System;
using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class UserTender
    {
        public int UserTenderId { get; set; }
        public User User { get; set; }
        public Tender Tender { get; set; }
        public DateTime? BeganWithProject { get; set; }
        public DateTime? EndedWithProject { get; set; }
        public ClientType UserType { get; set; }
        public string StatusOnProject { get; set; }

        public virtual IList<UserFile> UserFiles { get; set; }
        public virtual IList<Message> Messages { get; set; }
        public virtual IList<UserNotification> UserNotifications { get; set; }
    }
}
