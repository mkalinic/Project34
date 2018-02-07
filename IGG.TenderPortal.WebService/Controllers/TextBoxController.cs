using IGG.TenderPortal.WebService.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class TextBoxController : Controller
    {
        // GET: TextBox
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Save(TextBlock tb)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, tb);
        }


        [HttpPost]
        public ActionResult DeleteTextBoxFile(TextBlockFile tbf)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, tbf);
        }

        [HttpPost]
        public ActionResult Delete(TextBlock tb)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, tb);
        }


        [HttpGet]
        public async Task<string> SendFileUploadedEmail(/*string fileName, */string time, string language = "nl")
        {
            return "EMAILS_SENT_SUCESFULLY";
        }
    }
}