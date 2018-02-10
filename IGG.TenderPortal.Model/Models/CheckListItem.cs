namespace IGG.TenderPortal.Model
{
    public class CheckListItem
    {
        public int CheckListItemId { get; set; }
        public Tender Tender { get; set; }
        public string Value { get; set; }
        public int ItemOrder { get; set; }
        public bool Checked { get; set; }
        public int IDUser { get; set; }
    }
}
