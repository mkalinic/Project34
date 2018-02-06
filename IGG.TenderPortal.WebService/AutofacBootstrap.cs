using System;
using Autofac;
using Microsoft.AspNet.Identity.Owin;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Service;
using IGG.TenderPortal.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IGG.TenderPortal.WebService
{
    internal class AutofacBootstrap
    {
        internal static void Init(ContainerBuilder builder)            
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            // Services
            builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            var x = new ApplicationDbContext();
            builder.Register(c => x);
            builder.Register<UserStore<ApplicationUser>>(c => new UserStore<ApplicationUser>(x)).AsImplementedInterfaces();
            builder.Register<IdentityFactoryOptions<ApplicationUserManager>>(c => new IdentityFactoryOptions<ApplicationUserManager>()
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("ApplicationName")
            });
            builder.RegisterType<ApplicationUserManager>();
        }
    }
}