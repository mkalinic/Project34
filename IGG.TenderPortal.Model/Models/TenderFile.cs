namespace IGG.TenderPortal.Model
{
    public class TenderFile
    {
        public int TenderFileId { get; set; }
        public Tender Tender { get; set; }
        public string LocationPath { get; set; }
    }
}
