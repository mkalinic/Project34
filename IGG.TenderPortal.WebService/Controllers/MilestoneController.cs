using AutoMapper;
using IGG.TenderPortal.Service;
using IGG.TenderPortal.WebService.Models;
using System.Web.Mvc;
using Tenderingportal.Authorization;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class MilestoneController : Controller
    {
        private readonly IMilestoneService _milestoneService;
        private readonly ITenderService _tenderService;

        public MilestoneController(IMilestoneService milestoneService, ITenderService tenderService)
        {
            _milestoneService = milestoneService;
            _tenderService = tenderService;
        }

        [HttpPost]
        public ActionResult Delete(Models.Milestone value)
        {
            var milestone = _milestoneService.GetById(value.ID, value.IDproject);
            if (milestone == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, milestone);

            _milestoneService.Delete(milestone);
            _milestoneService.Save();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, value);
        }

        [HttpPost]
        [AuthorizationAFA(AllowedUserTypes = "IGG")]
        public ActionResult Save(Models.Milestone value)
        {
            Model.Milestone milestone;

            if (value.ID <= 0)
                milestone = CreateMilestone(value);
            else
                milestone = UpdateMilestone(value);

            var model = Mapper.Map<Model.Milestone, Models.Milestone>(milestone);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, model);

        }

        private Model.Milestone CreateMilestone(Models.Milestone value)
        {
            var milestone = Mapper.Map<Models.Milestone, Model.Milestone>(value);
            var tender = _tenderService.GetTenderById(value.IDproject);
            milestone.Tender = tender;
            _milestoneService.Create(milestone);
            _milestoneService.Save();

            return milestone;
        }

        private Model.Milestone UpdateMilestone(Models.Milestone value)
        {
            var milestone = _milestoneService.GetById(value.ID, value.IDproject);
            var tender = _tenderService.GetTenderById(value.IDproject);

            Mapper.Map(value, milestone);

            _milestoneService.Update(milestone);
            _milestoneService.Save();

            return milestone;
        }
    }
}