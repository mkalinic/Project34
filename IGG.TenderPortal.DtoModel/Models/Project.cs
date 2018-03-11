using System;
using System.Collections.Generic;

namespace IGG.TenderPortal.DtoModel
{
    public class Project
    {


        //public Project(bool initLists = false) {
        //    TextBlocks = new List<TextBlock>();
        //    Posts = new List<Post>();
        //    Milestones = new List<Milestone>();
        //    UsersProjects = new List<UsersProject>();
        //    ProjectFiles = new List<ProjectFile>();
        //}

        public Project()
        {
           
        }

        public int ID { get; set; } = -1;
        public string name { get; set; }
        public string description { get; set; }
        public string place { get; set; }
        public int clientCreated { get; set; }
        public int IGGperson { get; set; }
        public bool canUpload { get; set; }
        public string status { get; set; }
        public DateTime? submisionDate { get; set; }
        public string photo { get; set; }
        public bool? onFrontPage { get; set; }

        public DateTime? timeCompleted { get; set; }
        public DateTime? timeCreated { get; set; }
        public string photoThumbnail { get; set; }
        public DateTime? timeOpenVault { get; set; }

        
        //--- optional, Fetch if needed
        public List<TextBlock> TextBlocks { get; set; } = null;
        //--- optional, Fetch if needed
        public List<Post> Posts { get; set; } = null;
        //--- optional, Fetch if needed
        public List<Milestone> Milestones { get; set; } = null;
        //--- optional, Fetch if needed
        public List<Phase> Phases { get; set; } = null;
        //--- optional, Fetch if needed
        public List<UsersProject> UsersProjects { get; set; } = null;
        //--- optional, Fetch if needed
        public List<Notification> Notifications { get; set; } = null;
        //--- optional, Fetch if needed
        public List<ProjectFile> ProjectFiles { get; set; } = null;
        
        /// <summary>
        /// Client that created this
        /// </summary>
        public User Client { get; set; }

        public int? curentPhase { get; set; }

        public string clientName { get; set; }
    }
}