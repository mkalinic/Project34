using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Service;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using IGG.TenderPortal.Data;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace IGG.TenderPortal.WebService
{
    public class AutofacWebapiConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config, IAppBuilder app)
        {
            Initialize(config, RegisterServices(new ContainerBuilder(), app));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder, IAppBuilder app)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(TenderRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            // Services
            builder.RegisterAssemblyTypes(typeof(TenderService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            var x = new ApplicationDbContext();
            builder.Register(c => x);
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<Microsoft.Owin.Security.IAuthenticationManager>();            
            builder.Register<UserStore<ApplicationUser>>(c => new UserStore<ApplicationUser>(x)).AsImplementedInterfaces();
            builder.Register<RoleStore<IdentityRole>>(c => new RoleStore<IdentityRole>(x)).AsImplementedInterfaces();

            builder.Register<IdentityFactoryOptions<ApplicationUserManager>>(c => new IdentityFactoryOptions<ApplicationUserManager>()
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("ApplicationName")
            });
            builder.RegisterType<ApplicationUserManager>();


            //builder.RegisterType<ApplicationSignInManager>();

            //builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            //builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>();
            //builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            //builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

            //// Repositories
            //builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly)
            //    .As<IEmployeeRepository>()                
            //    .AsImplementedInterfaces().InstancePerRequest();

            //// Services
            //builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly)
            //    .As<IEmployeeService>()
            //    .AsImplementedInterfaces().InstancePerRequest();

            //Set the dependency resolver to be Autofac.

            builder.RegisterControllers(Assembly.GetExecutingAssembly()); //Register MVC Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

            Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container)); //Set the MVC DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)Container); //Set the WebApi DependencyResolver

            //app.UseAutofacMiddleware(Container);
            //app.UseAutofacMvc();

            return Container;
        }

    }
}