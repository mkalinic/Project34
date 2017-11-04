using System.Data.Entity.ModelConfiguration;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Configuration
{
    public class UserFileConfiguration : EntityTypeConfiguration<UserFile>
    {
        public UserFileConfiguration()
        {
            ToTable("UserTender");

            HasKey(s => s.UserFileId);

            HasRequired(s => s.UserTender)
                .WithMany(m => m.UserFiles)
                .HasForeignKey(f => f.UserFileId);                
        }
    }
}