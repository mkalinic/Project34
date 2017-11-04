using System.Collections.Generic;

namespace IGG.TenderPortal.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public virtual IList<Logging> Loggings { get; set; }
    }
}
