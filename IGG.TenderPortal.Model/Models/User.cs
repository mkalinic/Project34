using IGG.TenderPortal.Common;
using System;
using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class User
    {
        public int UserId { get; set; }        
        public ClientType Type { get; set; }
        public bool Sex { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Section { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string MiddleName { get; set; }
        public string PostCode { get; set; }
        public DateTime? Joined { get; set; }
        public virtual IList<Logging> Loggings { get; set; }
    }
}
