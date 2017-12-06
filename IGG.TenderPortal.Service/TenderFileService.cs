using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;
using System.Collections.Generic;
using System;

namespace IGG.TenderPortal.Service
{
    public interface ITenderFileService
    {
        IEnumerable<TenderFile> GetByBlockId(int blockId);
        TenderFile GetById(int id, int blockId);
        void Create(TenderFile tenderFile);
        void Update(TenderFile tenderFile);
        void Delete(TenderFile tenderFile);
        void Save();
    }

    public class TenderFileService : ITenderFileService
    {
           private readonly ITenderFileRepository _tenderFileRepository;
           private readonly IUnitOfWork _unitOfWork;

        public TenderFileService(ITenderFileRepository tenderFileRepository, IUnitOfWork unitOfWork)
        {
            _tenderFileRepository = tenderFileRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TenderFile> GetByBlockId(int blockId)
        {
            var tenderFiles = _tenderFileRepository.GetByBlockId(blockId);
            return tenderFiles;
        }

        public TenderFile GetById(int id, int blockId)
        {
            var tenderFile = _tenderFileRepository.GetById(id, blockId);
            return tenderFile;
        }

        public void Create(TenderFile tenderFile)
        {
            _tenderFileRepository.Add(tenderFile);
        }
        public void Update(TenderFile tenderFile)
        {
            _tenderFileRepository.Update(tenderFile);
        }

        public void Delete(TenderFile tenderFile)
        {
            _tenderFileRepository.Delete(tenderFile);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
