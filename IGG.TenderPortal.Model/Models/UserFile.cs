namespace IGG.TenderPortal.Model
{
    public class UserFile
    {
        public int UserFileId { get; set; }
        public UserTender UserTender { get; set; }
        public string LocationPath { get; set; }
    }
}
