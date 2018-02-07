using System;

namespace IGG.TenderPortal.WebService.Models
{
    /// <summary>
    /// This is where 
    /// </summary>
    public class UsersProject
    {
        public int ID { get; set; }
        public int IDproject { get; set; }
        public int IDuser { get; set; }
        public DateTime? beganWithProject { get; set; }
        public DateTime? endedWithProject { get; set; }
        public string statusOnProject { get; set; }
        public string userType { get; set; }

        //--- optional, Fetch if needed
        public Project Project { get; set; } = null;
        //--- optional, Fetch if needed
        public User User { get; set; } = null;

    }
}