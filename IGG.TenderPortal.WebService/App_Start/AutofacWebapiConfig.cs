﻿using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Service;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using IGG.TenderPortal.Data;

namespace IGG.TenderPortal.WebService
{
    public class AutofacWebapiConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

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

            //// Repositories
            //builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly)
            //    .As<IEmployeeRepository>()                
            //    .AsImplementedInterfaces().InstancePerRequest();

            //// Services
            //builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly)
            //    .As<IEmployeeService>()
            //    .AsImplementedInterfaces().InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }
}