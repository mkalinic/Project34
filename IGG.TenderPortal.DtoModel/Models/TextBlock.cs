using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IGG.TenderPortal.DtoModel
{
    public class TextBlock
    {

        public int ID { get; set; } = -1;
        public int IDproject { get; set; }
        public string text { get; set; }
        public List<TextBlockFile> Files { get; set; }
        public DateTime? time { get; set; }
    }
}