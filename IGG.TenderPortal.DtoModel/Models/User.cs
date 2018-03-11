using System;

namespace IGG.TenderPortal.DtoModel
{
    public class User
    {
        public int ID { get; set; } = -1;
        public string username { get; set; }
        public string pass { get; set; }
        public bool sex { get; set; }
        public string initials { get; set; }
        public string insertion { get; set; }
        public string surname { get; set; }
        public string titleFront { get; set; }
        public string titleRear { get; set; }
        public string title { get; set; }
        public string companyName { get; set; }
        public string section { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string userType { get; set; }
        public string photo { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string postCode { get; set; }
        public DateTime? joined { get; set; }

    }
}