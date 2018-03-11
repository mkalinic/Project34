using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class LogbookController : Controller
    {
        // GET: Logbook
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAll(string id, int? IDproject, DateTime? StartDate, DateTime? EndDate, int page, int pagesize)// id is usertype
        {
            List<Logbook> posts = GetDummyLogbooks();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, posts);

        }

        [HttpPost]
        public ActionResult GetCount(string id, int? IDproject, DateTime? StartDate, DateTime? EndDate)// id is usertype
        {
            int count = 12;
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, count);

        }
        
        [HttpPost]
        public ActionResult GetAllForUser(int id, int? IDproject, DateTime? StartDate, DateTime? EndDate, int page, int pagesize)// id is usertype
        {
            List<Logbook> posts = GetDummyLogbooks();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, posts);

        }

        [HttpPost]
        public ActionResult GetCountForUser(string id, int? IDproject, DateTime? StartDate, DateTime? EndDate)// id is usertype
        {
            int count = 12;
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, count);

        }

        private List<Logbook> GetDummyLogbooks()
        {
            return new List<Logbook>
            {
                GetDummyLogbook(),
                GetDummyLogbook(),
                GetDummyLogbook()
            };
        }

        private static Logbook GetDummyLogbook()
        {
            return new Logbook
            {
                filename = "testfilename",
                name = "testname",
                projectName = "TestProject",
            };
        }
    }
}