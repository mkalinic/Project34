using System;
using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class Tender
    {
        public int TenderId { get; set; }
        public string ProjectName { get; set; }
        public string Client { get; set; }
        public string Description { get; set; }
        public byte[] PhotoContent { get; set; }
        public DateTime SubmissionDate { get; set; }
        public TenderStatus Status { get; set; }
        public Phase Phase { get; set; }
        public bool DigitalSubmission { get; set; }
        public bool ViewOnHomepage { get; set; }
        public bool IsCompleted { get; set; }

        public virtual IList<Milestone> Milestones { get; set; }
        public virtual IList<TenderFile> TenderFiles { get; set; }
        public virtual IList<CheckList> CheckLists { get; set; }
    }
}
