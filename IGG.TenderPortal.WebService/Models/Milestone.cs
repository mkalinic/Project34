using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IGG.TenderPortal.WebService.Models
{
    public class Milestone
    {

        public int ID { get; set; } = -1;
        public int IDproject { get; set; }
        public string name { get; set; }
        public string visibleFor { get; set; }
        public DateTime? time { get; set; }
        public string notificationTo { get; set; }
        public DateTime? notificationDate { get; set; }

    }
}