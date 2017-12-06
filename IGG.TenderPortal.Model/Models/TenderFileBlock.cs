using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class TenderFileBlock
    {
        public int TenderFileBlockId { get; set; }
        public Tender Tender { get; set; }
        public virtual IList<TenderFile> TenderFiles { get; set; }
    }
}
