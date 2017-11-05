using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Service
{
    public interface IUserTenderService
    {
        IEnumerable<UserTender> GetUserTenders();
        UserTender GetUserTenderByIds(int userId, int tenderId);       
    }

    public class UserTenderService : IUserTenderService
    {
        private readonly IUserTenderRepository _userTenderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserTenderService(IUserTenderRepository userTenderRepository, IUnitOfWork unitOfWork)
        {
            _userTenderRepository = userTenderRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserTender> GetUserTenders()
        {
            var userTenders = _userTenderRepository.GetUserTenders();
            return userTenders;
        }

        public UserTender GetUserTenderByIds(int userId, int tenderId) 
        {
            var userTender = _userTenderRepository.GetUserTenderByIds(userId, tenderId);
            return userTender;
        }
    }
}
