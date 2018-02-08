using AutoMapper;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;
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
        private readonly IMilestoneService _milestoneService;
        private readonly ITenderService _tenderService;

        public MilestoneController(IMilestoneService milestoneService, ITenderService tenderService)
        {
            _milestoneService = milestoneService;
            _tenderService = tenderService;
        }

        // GET: Milestone
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Models.Milestone milestone)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, milestone);

        }

        [HttpPost]
        [AuthorizationAFA(AllowedUserTypes = "IGG")]
        public ActionResult Save(Models.Milestone value)
        {
            if (value.ID <= 0)
                CreateMilestone(value);
            else
                UpdateMilestone(value);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, value);

        }

        private void CreateMilestone(Models.Milestone value)
        {
            var milestone = Mapper.Map<Models.Milestone, Model.Milestone>(value);
            var tender = _tenderService.GetTenderById(value.IDproject);
            milestone.Tender = tender;
            _milestoneService.Create(milestone);
            _milestoneService.Save();
        }

        private void UpdateMilestone(Models.Milestone value)
        {
            var milestone = _milestoneService.GetById(value.ID, value.IDproject);
            var tender = _tenderService.GetTenderById(value.IDproject);

            Mapper.Map(value, milestone);

            _milestoneService.Update(milestone);
            _milestoneService.Save();
        }
    }
}