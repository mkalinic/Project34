using GG.TenderPortal.WebService.Models;
using IGG.TenderPortal.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class ChecklistController : Controller
    {
        // GET: Checklist
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetForProject(int id)
        {
            List<Checklist> chlist =  GetDummyChecklists() ;
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, chlist);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }



        [HttpGet]
        public ActionResult ChangeOrder(int id, int order)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }


        [HttpPost]
        public ActionResult Save(Checklist chlist)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }





        [HttpPost]
        public ActionResult SaveChecked(ChecklistChecked chlist)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }


        [HttpPost]
        [AuthorizationAFA(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public ActionResult RemoveChecked(ChecklistChecked chlist)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }

        private List<Checklist> GetDummyChecklists()
        {
            return new List<Checklist>
            {
                GetDummyChecklist()
            };
        }

        private Checklist GetDummyChecklist()
        {
            return new Checklist
            {
                Checked = true,
                projectID = 1
            };
        }
    }
}