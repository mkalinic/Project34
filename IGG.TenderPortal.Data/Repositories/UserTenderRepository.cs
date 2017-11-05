using System.Collections.Generic;
using System.Linq;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Repositories
{
    public class UserTenderRepository : RepositoryBase<UserTender>, IUserTenderRepository
    {
        public UserTenderRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<UserTender> GetUserTenders()
        {
            return DbContext
             .UserTender
             .Include("User")
             .Include("Tender")
             .ToList();
        }

        public UserTender GetUserTenderByIds(int userId, int tenderId)
        {
            return DbContext
                .UserTender
                .Include("User")
                .Include("Tender")
                .SingleOrDefault(e => e.User.UserId == userId 
                && e.Tender.TenderId == tenderId);
        }
    }

    public interface IUserTenderRepository : IRepository<UserTender>
    {
        IEnumerable<UserTender> GetUserTenders();
        UserTender GetUserTenderByIds(int userId, int tenderId);
    }
}
