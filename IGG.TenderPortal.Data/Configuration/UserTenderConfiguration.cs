using System.Data.Entity.ModelConfiguration;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Configuration
{
    public class UserTenderConfiguration : EntityTypeConfiguration<UserTender>
    {
        public UserTenderConfiguration()
        {
            ToTable("UserTender");

            HasKey(s => s.UserTenderId);

            HasRequired(s => s.Tender);

            HasRequired(s => s.User);
        }
    }
}
