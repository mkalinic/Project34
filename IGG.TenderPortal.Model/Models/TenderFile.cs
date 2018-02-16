using System;

namespace IGG.TenderPortal.Model
{
    public class TenderFile
    {
        public int TenderFileId { get; set; }
        public TenderFileBlock TenderFileBlock { get; set; }        
        public string LocationPath { get; set; }
        public string DisplayName { get; set; }        
        public DateTime DateUploaded { get; set; }
        public DateTime? DateModified { get; set; } = null;
        public int Size { get; set; } = 0;
    }
}
