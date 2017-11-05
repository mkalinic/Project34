using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Service
{
    public interface ITenderService
    {
        IEnumerable<Tender> GetTenders();
        Tender GetTenderById(int tenderId);
        void CreateTender(Tender tender);
        void UpdateTender(Tender tender);
        void RemoveTender(Tender tender);
        void SaveTender();
    }

    public class TenderService : ITenderService
    {
        private readonly ITenderRepository _tenderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TenderService(ITenderRepository tenderRepository, IUnitOfWork unitOfWork)
        {
            _tenderRepository = tenderRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Tender> GetTenders()
        {
            var userTenders = _tenderRepository.GetTenders();
            return userTenders;
        }

        public Tender GetTenderById(int tenderId) 
        {
            var tender = _tenderRepository.GetTenderById(tenderId);
            return tender;
        }

        public void CreateTender(Tender tender)
        {
            _tenderRepository.Add(tender);
        }

        public void UpdateTender(Tender tender)
        {
            _tenderRepository.Update(tender);
        }

        public void RemoveTender(Tender tender)
        {
            _tenderRepository.Delete(tender);
        }

        public void SaveTender()
        {
            _unitOfWork.Commit();
        }
    }
}
