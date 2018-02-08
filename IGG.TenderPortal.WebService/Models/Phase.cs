using System;

namespace IGG.TenderPortal.WebService.Models
{
    public class Phase
    {
        public int ID { get; set; } = -1;
        public int? IDproject { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
    }
}