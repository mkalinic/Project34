namespace IGG.TenderPortal.Model
{
    public class TenderFile
    {
        public int TenderFileId { get; set; }
        public TenderFileBlock TenderFileBlock { get; set; }        
        public string LocationPath { get; set; }
    }
}
