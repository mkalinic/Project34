using System.Data.Entity;
using IGG.TenderPortal.Data.Configuration;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data
{
    public class PortalEntities : DbContext
    {
        public PortalEntities() : base("PortalEntities") { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Person> Person { get; set; }
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
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
        }
    }
}
