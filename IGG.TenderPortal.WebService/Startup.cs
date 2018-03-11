using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using IGG.TenderPortal.Data;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Service;
using Autofac.Integration.WebApi;
using System.Reflection;

[assembly: OwinStartup(typeof(IGG.TenderPortal.WebService.Startup))]

namespace IGG.TenderPortal.WebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // REGISTER REPOSITORIES
            builder.RegisterAssemblyTypes(typeof(TenderRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            // REGISTER SERVICES
            builder.RegisterAssemblyTypes(typeof(TenderService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            // REGISTER DEPENDENCIES
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // BUILD THE CONTAINER
            var container = builder.Build();            

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // FOR WEB API
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // REGISTER WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();            
            app.UseAutofacWebApi(config);            
            app.UseWebApi(config);

            ConfigureAuth(app);
        }
    }
}
