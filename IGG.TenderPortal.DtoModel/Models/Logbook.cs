using System;

namespace IGG.TenderPortal.DtoModel
{
    public class Logbook
    {
        public int ID { get; set; } = -1;
        public int userID { get; set; }
        public int? projectID { get; set; }
        public string projectName { get; set; }
        public string filename { get; set; }
        public string action { get; set; }

        public string textbox { get; set; }
        public DateTime? time { get; set; }

        public string surname { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string initials{ get; set; }
 

        public static readonly string ACTION_LOGGED_IN = "Login";
        public static readonly string ACTION_PROJECT_OPENED = "Open project";
        public static readonly string ACTION_PROJECT_FILE_DOWNLOADED = "Download";
        public static readonly string ACTION_PROJECT_ZIP_FILE_DOWNLOADED = "Complete ZIP download";
        public static readonly string ACTION_USERDATA_CHANGED = "Change user ";
 

    }
}