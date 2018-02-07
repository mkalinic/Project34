using IGG.TenderPortal.Common;
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
        public byte[] Photo { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public TenderStatus Status { get; set; }
        public Phase Phase { get; set; }
        public bool DigitalSubmission { get; set; }
        public bool ViewOnHomepage { get; set; }
        public bool IsCompleted { get; set; }

        public virtual IList<Milestone> Milestones { get; set; }
        public virtual IList<TenderFileBlock> TenderFileBlocks { get; set; }
        public virtual IList<CheckList> CheckLists { get; set; }

        public string Place { get; set; }
        public int ClientCreated { get; set; }
        public int IGGperson { get; set; }
        public bool CanUpload { get; set; }
        public DateTime? TimeCompleted { get; set; }
        public DateTime? TimeCreated { get; set; }
        public string PhotoThumbnail { get; set; }
        public DateTime? TimeOpenVault { get; set; }
    }
}
