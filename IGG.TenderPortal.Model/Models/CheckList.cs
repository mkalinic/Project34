using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class CheckList
    {
        public int ChecklistId { get; set; }
        public Tender Tender { get; set; }
        //public virtual IList<CheckListItem> Items { get; set; }
    }
}
