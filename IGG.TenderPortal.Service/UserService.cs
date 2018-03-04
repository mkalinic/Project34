using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        User GetUserByGuid(string guid);
        void CreateUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
        void SaveUser();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users;
        }

        public User GetUserById(int userId) 
        {
            var user = _userRepository.GetUserByIds(userId);
            return user;
        }

        public User GetUserByGuid(string guid)
        {
            var user = _userRepository.GetUserByGuid(guid);
            return user;
        }

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void RemoveUser(User user)
        {
            _userRepository.Delete(user);
        }

        public void SaveUser()
        {
            _unitOfWork.Commit();
        }
    }
}
