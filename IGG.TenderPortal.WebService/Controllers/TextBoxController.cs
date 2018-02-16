using IGG.TenderPortal.Service;
using IGG.TenderPortal.WebService.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using AutoMapper;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class TextBoxController : Controller
    {
        private readonly IUserTenderService _userTenderService;
        private readonly IUserService _userService;
        private readonly ITenderService _tenderService;
        private readonly ITenderFileBlockService _tenderFileBlockService;

        public TextBoxController(ITenderService tenderService, IUserTenderService userTenderService, IUserService userService, ITenderFileBlockService tenderFileBlockService)
        {
            _userTenderService = userTenderService;
            _userService = userService;
            _tenderService = tenderService;
            _tenderFileBlockService = tenderFileBlockService;
        }

        // GET: TextBox
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Save(TextBlock tb)
        {
            if (tb.ID <= 0)
                CreateTenderFileBlock(tb);
            else
                UpdateTenderFileBlock(tb);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, tb);
        }

        private void UpdateTenderFileBlock(TextBlock tb)
        {
            var tenderFileBlock = _tenderFileBlockService.GetById(tb.ID, tb.IDproject);

            Mapper.Map(tb, tenderFileBlock);

            _tenderFileBlockService.Update(tenderFileBlock);
            _tenderFileBlockService.Save();
        }

        private void CreateTenderFileBlock(TextBlock tb)
        {
            var tenderFileBlock = Mapper.Map<TextBlock, TenderFileBlock>(tb);

            var tender = _tenderService.GetTenderById(tb.IDproject);
            tenderFileBlock.Tender = tender;

            _tenderFileBlockService.Create(tenderFileBlock);
            _tenderFileBlockService.Save();
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