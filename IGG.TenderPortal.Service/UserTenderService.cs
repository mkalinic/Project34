using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Service
{
    public interface IUserTenderService
    {
        IEnumerable<UserTender> GetUserTenders();
        UserTender GetUserTenderById(int userTenderId);
        UserTender GetUserTenderByIds(int userId, int tenderId);
        void CreateUserTender(UserTender userTender);
        void UpdateUserTender(UserTender userTender);
        void RemoveUserTender(UserTender userTender);
        void SaveUserTender();
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
        public UserTender GetUserTenderById(int userTenderId)
        {
            var userTender = _userTenderRepository.GetById(userTenderId);
            return userTender;
        }
        public UserTender GetUserTenderByIds(int userId, int tenderId) 
        {
            var userTender = _userTenderRepository.GetUserTenderByIds(userId, tenderId);
            return userTender;
        }
        public void CreateUserTender(UserTender userTender)
        {
            _userTenderRepository.Add(userTender);
        }
        public void UpdateUserTender(UserTender userTender)
        {
            _userTenderRepository.Update(userTender);
        }
        public void RemoveUserTender(UserTender userTender)
        {
            _userTenderRepository.Delete(userTender);
        }
        public void SaveUserTender()
        {
            _unitOfWork.Commit();
        }
    }
}
