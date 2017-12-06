using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Service
{
    public interface IMilestoneService
    {
        IEnumerable<Milestone> GetByTenderId(int tenderId);
        Milestone GetById(int id, int tenderId);
        void Create(Milestone milestone);
        void Update(Milestone milestone);
        void Delete(Milestone milestone);
        void Save();

    }

    public class MilestoneService : IMilestoneService
    {
        private readonly IMilestoneRepository _milestoneRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MilestoneService(IMilestoneRepository milestoneRepository, IUnitOfWork unitOfWork)
        {
            _milestoneRepository = milestoneRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Milestone> GetByTenderId(int tenderId)
        {
            var milestones = _milestoneRepository.GetByTenderId(tenderId);
            return milestones;
        }

        public Milestone GetById(int id, int tenderId)
        {
            var milestone = _milestoneRepository.GetById(id, tenderId);
            return milestone;
        }

        public void Create(Milestone milestone)
        {
            _milestoneRepository.Add(milestone);            
        }

        public void Update(Milestone milestone)
        {
            _milestoneRepository.Update(milestone);            
        }

        public void Delete(Milestone milestone)
        {
            _milestoneRepository.Delete(milestone);        
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
