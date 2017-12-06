using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;
using System.Collections.Generic;
using System;

namespace IGG.TenderPortal.Service
{
    public interface ITenderFileBlockService
    {
        IEnumerable<TenderFileBlock> GetByTenderId(int tenderId);
        TenderFileBlock GetById(int id, int tenderId);
        void Create(TenderFileBlock tenderFileBlock);
        void Update(TenderFileBlock tenderFileBlock);
        void Delete(TenderFileBlock tenderFileBlock);
        void Save();
    }

    public class TenderFileBlockService : ITenderFileBlockService
    {
           private readonly ITenderFileBlockRepository _tenderFileBlockRepository;
           private readonly IUnitOfWork _unitOfWork;

        public TenderFileBlockService(ITenderFileBlockRepository tenderFileBlockRepository, IUnitOfWork unitOfWork)
        {
            _tenderFileBlockRepository = tenderFileBlockRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TenderFileBlock> GetByTenderId(int tenderId)
        {
            var tenderFileBlocks = _tenderFileBlockRepository.GetByTenderId(tenderId);
            return tenderFileBlocks;
        }

        public TenderFileBlock GetById(int id, int tenderId)
        {
            var tenderFileBlock = _tenderFileBlockRepository.GetById(id, tenderId);
            return tenderFileBlock;
        }

        public void Create(TenderFileBlock tenderFileBlock)
        {
            _tenderFileBlockRepository.Add(tenderFileBlock);
        }
        public void Update(TenderFileBlock tenderFileBlock)
        {
            _tenderFileBlockRepository.Update(tenderFileBlock);
        }

        public void Delete(TenderFileBlock tenderFileBlock)
        {
            _tenderFileBlockRepository.Delete(tenderFileBlock);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
