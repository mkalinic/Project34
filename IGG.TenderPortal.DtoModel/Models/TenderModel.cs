using IGG.TenderPortal.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace IGG.TenderPortal.DtoModel
{
    public class TenderModel
    {
        public int TenderId { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string Client { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public DateTime SubmissionDate { get; set; }
        public TenderStatus Status { get; set; }
        public Phase Phase { get; set; }
        public bool DigitalSubmission { get; set; }
        public bool ViewOnHomepage { get; set; }
        public bool IsCompleted { get; set; }
    }
}
