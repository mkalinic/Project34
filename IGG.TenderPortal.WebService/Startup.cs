using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(IGG.TenderPortal.WebService.Startup))]

namespace IGG.TenderPortal.WebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Bootstrapper.Run();
            //HttpConfiguration config = new HttpConfiguration();
            //WebApiConfig.Register(config);
            //app.UseWebApi(config);
            ConfigureAuth(app);
        }
    }
}
