namespace IGG.TenderPortal.Model
{
    public class Message
    {
        public int Messageid { get; set; }
        public string Text { get; set; }
        public UserTender UserTender { get; set; }
    }
}
