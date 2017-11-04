using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class Tender
    {
        public int TenderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TenderStatus Status { get; set; }
        public bool IsClosed { get; set; }
        public virtual IList<Milestone> Milestones { get; set; }
        public virtual IList<TenderFile> TenderFiles { get; set; }
        public virtual IList<CheckList> CheckLists { get; set; }
    }
}
