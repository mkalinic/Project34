using System.Collections.Generic;
using System.Linq;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<User> GetUsers()
        {
            return DbContext
             .User
             .ToList();
        }

        public User GetUserByIds(int userId)
        {
            return DbContext
                .User
                .SingleOrDefault(e => e.UserId == userId);
        }
    }

    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers();
        User GetUserByIds(int userId);
    }
}
