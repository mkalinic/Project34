using System;

namespace IGG.TenderPortal.DtoModel
{
    public class TextBlockFile
    {
        public int ID { get; set; } = -1;
        public int IDTextBlock { get; set; }
        public string displayName { get; set; }
        public string file { get; set; }
        public DateTime dateUploaded { get; set; }
        public DateTime? dateModified { get; set; } = null;
        public int size { get; set; } = 0;
    }
}