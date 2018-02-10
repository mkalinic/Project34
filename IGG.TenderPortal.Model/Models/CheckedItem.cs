namespace IGG.TenderPortal.Model
{
    public class CheckedItem
    {
        public int CheckedItemId { get; set; }
        public CheckListItem CheckListItem { get; set; }
        public User User { get; set; }        
    }
}
