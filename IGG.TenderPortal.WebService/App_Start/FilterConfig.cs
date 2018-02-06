using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;

namespace IGG.TenderPortal.WebService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        //    filters.Add(new AuthenticationAFA());
 
    }
    }
}
