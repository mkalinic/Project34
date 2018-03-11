using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using IGG.TenderPortal.Data;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IGG.TenderPortal.WebService
{
     public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {            
        }
        
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IdentityFactoryOptions<ApplicationUserManager> options)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }            
        }
    }
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<System.Security.Claims.ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }
    }
}
