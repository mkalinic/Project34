using IGG.TenderPortal.WebService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> GetTopN(int n)
        {
            var retObj = GetDummyNotification();
 
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, retObj);
        }

        [HttpGet]
        public ActionResult GetLatestNotificationForProject(int IDproject, int howmany)
        {
            List<Notification> list = GetDummyNotifications();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, list);
        }

       [HttpPost]
        public ActionResult Write(Notification notification)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, notification);
        }

        private List<Notification> GetDummyNotifications()
        {
            return new List<Notification>
            {
                GetDummyNotification(),
                GetDummyNotification(),
                GetDummyNotification()
            };
        }

        private Notification GetDummyNotification()
        {
            return new Notification
            {
                message = "message1"
            };
        }
    }
}