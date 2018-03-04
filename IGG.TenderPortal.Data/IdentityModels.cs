using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Data.Configuration;

namespace IGG.TenderPortal.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ApplicationIdentity", throwIfV1Schema: false)
        {
        }
        public DbSet<Tender> Tender { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserTender> UserTender { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Milestone> Milestone { get; set; }
        public DbSet<TenderFileBlock> TenderFileBlock { get; set; }
        public DbSet<TenderFile> TenderFile { get; set; }
        public DbSet<CheckListItem> CheckListItem { get; set; }
        public DbSet<CheckedItem> CheckedItem { get; set; }

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