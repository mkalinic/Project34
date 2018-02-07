using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;
using IGG.TenderPortal.WebService.Models;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class TenderController : ApiController
    {        
        private readonly ITenderService _tenderService;

        public TenderController(ITenderService tenderService)
        {            
            _tenderService = tenderService;
        }

        // GET: api/Tender
        public IEnumerable<TenderModel> Get()
        {
            var tenders = _tenderService.GetTenders();
            return Mapper.Map<IEnumerable<Tender>, IEnumerable<TenderModel>>(tenders);
        }

        // GET: api/Tender/5
        public TenderModel Get(int id)
        {
            var tender = _tenderService.GetTenderById(id);
            return Mapper.Map<Tender, TenderModel>(tender);
        }

        // POST: api/Tender
        public void Post([FromBody]TenderModel value)
        {
            var tender = Mapper.Map<TenderModel, Tender>(value);

            _tenderService.CreateTender(tender);
            _tenderService.SaveTender();
        }

        // PUT: api/Tender/5
        public void Put(int id, [FromBody]TenderModel value)
        {
            var tender = _tenderService.GetTenderById(id);

            Mapper.Map(value, tender);

            _tenderService.UpdateTender(tender);
            _tenderService.SaveTender();
        }

        // DELETE: api/Tender/5
        public void Delete(int id)
        {
            var tender = _tenderService.GetTenderById(id);

            _tenderService.RemoveTender(tender);
            _tenderService.SaveTender();
        }
    }
}
