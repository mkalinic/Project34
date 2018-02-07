using IGG.TenderPortal.Model;
using IGG.TenderPortal.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class MilestoneController : Controller
    {
        // GET: Milestone
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(IGG.TenderPortal.WebService.Models.Milestone milestone)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, milestone);

        }

        [HttpPost]
        [AuthorizationAFA(AllowedUserTypes = "IGG")]
        public ActionResult Save(IGG.TenderPortal.WebService.Models.Milestone milestone)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, milestone);

        }
    }
}