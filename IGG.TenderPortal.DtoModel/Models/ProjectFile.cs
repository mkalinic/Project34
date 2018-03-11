using System;

namespace IGG.TenderPortal.DtoModel
{
    public class ProjectFile
    {

        public int ID { get; set; } = -1;
        public int IDproject { get; set; } = -1;
        public string displayName { get; set; }
        public string file { get; set; }
        public DateTime dateUploaded { get; set; }
        public DateTime? dateModified { get; set; } = null;
        public int size { get; set; } = 0;
        public bool userCanDelete { get; set; }
        public int IDuser { get; set; }

    }
}