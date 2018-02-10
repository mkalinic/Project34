using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Repositories
{
    public class CheckedItemRepository : RepositoryBase<CheckedItem>, ICheckedItemRepository
    {
        public CheckedItemRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ICheckedItemRepository : IRepository<CheckedItem>
    {

    }
}