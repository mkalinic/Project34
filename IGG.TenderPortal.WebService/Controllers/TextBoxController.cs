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
        private readonly ITenderService _tenderService;
        private readonly ITenderFileBlockService _tenderFileBlockService;
        private readonly ITenderFileService _tenderFileService;

        public TextBoxController(ITenderService tenderService, ITenderFileBlockService tenderFileBlockService, ITenderFileService tenderFileService)
        {            
            _tenderService = tenderService;
            _tenderFileBlockService = tenderFileBlockService;
            _tenderFileService = tenderFileService;
        }

        [HttpPost]
        public ActionResult Save(TextBlock tb)
        {
            TenderFileBlock tenderFileBlock;

            if (tb.ID <= 0)
                tenderFileBlock = CreateTenderFileBlock(tb);
            else
                tenderFileBlock = UpdateTenderFileBlock(tb);

            var model = Mapper.Map<TenderFileBlock, TextBlock>(tenderFileBlock);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, model);
        }

        private TenderFileBlock UpdateTenderFileBlock(TextBlock tb)
        {
            var tenderFileBlock = _tenderFileBlockService.GetById(tb.ID, tb.IDproject);

            Mapper.Map(tb, tenderFileBlock);

            _tenderFileBlockService.Update(tenderFileBlock);
            _tenderFileBlockService.Save();

            return tenderFileBlock;
        }

        private TenderFileBlock CreateTenderFileBlock(TextBlock tb)
        {
            var tenderFileBlock = Mapper.Map<TextBlock, TenderFileBlock>(tb);

            var tender = _tenderService.GetTenderById(tb.IDproject);
            tenderFileBlock.Tender = tender;

            _tenderFileBlockService.Create(tenderFileBlock);
            _tenderFileBlockService.Save();

            return tenderFileBlock;
        }

        [HttpPost]
        public ActionResult DeleteTextBoxFile(TextBlockFile tbf)
        {
            var tenderFile = _tenderFileService.GetById(tbf.ID, tbf.IDTextBlock);
            if (tenderFile == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, tbf);

            _tenderFileService.Delete(tenderFile);
            _tenderFileService.Save();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, tbf);
        }

        [HttpPost]
        public ActionResult Delete(TextBlock tb)
        {
            var tenderFileBlock = _tenderFileBlockService.GetById(tb.ID, tb.IDproject);
            if(tenderFileBlock == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, tb);

            _tenderFileBlockService.Delete(tenderFileBlock);
            _tenderFileBlockService.Save();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, tb);
        }


        [HttpGet]
        public async Task<string> SendFileUploadedEmail(/*string fileName, */string time, string language = "nl")
        {
            throw new NotImplementedException();
        }
    }
}