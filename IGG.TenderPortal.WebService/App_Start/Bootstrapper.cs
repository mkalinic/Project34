using System.Web.Http;
using IGG.TenderPortal.WebService.Mapping;
using Owin;

namespace IGG.TenderPortal.WebService
{
    public static class Bootstrapper
    {
        public static void Run()
        {            
            AutoMapperConfiguration.Configure();
        }
    }
}