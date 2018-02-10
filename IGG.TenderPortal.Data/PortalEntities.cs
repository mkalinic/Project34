using System.Data.Entity;
using IGG.TenderPortal.Data.Configuration;
using IGG.TenderPortal.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using IGG.TenderPortal.Model.Identity;

namespace IGG.TenderPortal.Data
{
    public class PortalEntities : DbContext
    {
        public PortalEntities() : base("PortalEntities") { }

        public DbSet<Tender> Tender { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserTender> UserTender { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Milestone> Milestone { get; set; }
        public DbSet<TenderFileBlock> TenderFileBlock { get; set; }
        public DbSet<TenderFile> TenderFile { get; set; }

        public virtual void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
