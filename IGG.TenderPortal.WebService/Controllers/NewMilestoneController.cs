using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;
using System.Collections.Generic;
using System.Web.Http;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class NewMilestoneController : ApiController
    {
        private readonly IMilestoneService _milestoneService;
        private readonly ITenderService _tenderService;

        public NewMilestoneController(IMilestoneService milestoneService, ITenderService tenderService)
        {
            _milestoneService = milestoneService;
            _tenderService = tenderService;
        }
        // GET: api/Milestone
        public IEnumerable<MilestoneModel> Get(int tenderId)
        {
            var milestones = _milestoneService.GetByTenderId(tenderId);
            return Mapper.Map<IEnumerable<Model.Milestone>, IEnumerable<MilestoneModel>>(milestones);
        }

        // GET: api/Milestone/5
        public MilestoneModel Get(int id, int tenderId)
        {
            var milestone = _milestoneService.GetById(id, tenderId);
            return Mapper.Map<Model.Milestone, MilestoneModel>(milestone);
        }

        // POST: api/Milestone
        public void Post([FromBody]MilestoneModel value, int tenderId)
        {
            var milestone = Mapper.Map<MilestoneModel, Model.Milestone>(value);
            var tender = _tenderService.GetTenderById(tenderId);
            milestone.Tender = tender;
            _milestoneService.Create(milestone);
            _milestoneService.Save();
        }

        // PUT: api/Milestone/5
        public void Put(int id, [FromBody]MilestoneModel value, int tenderId)
        {
            var milestone = _milestoneService.GetById(id, tenderId);
            var tender = _tenderService.GetTenderById(tenderId);

            Mapper.Map(value, milestone);

            _milestoneService.Update(milestone);
            _milestoneService.Save();
        }

        // DELETE: api/Milestone/5
        public void Delete(int id, int tenderId)
        {
            var milestone =_milestoneService.GetById(id, tenderId);
            _milestoneService.Delete(milestone);
            _milestoneService.Save();
        }
    }
}
