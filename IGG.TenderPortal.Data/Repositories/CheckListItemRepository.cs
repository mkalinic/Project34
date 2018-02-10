using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;
using System.Collections.Generic;
using System;
using System.Linq;

namespace IGG.TenderPortal.Data.Repositories
{
    public class CheckListItemRepository : RepositoryBase<CheckListItem>, ICheckListItemRepository
    {
        public CheckListItemRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<CheckListItem> GetItemsByTenderId(int tenderId)
        {
            return DbContext
             .CheckListItem
             .Include("Tender")
             .Where(c => c.Tender.TenderId == tenderId);           
        }
    }

    public interface ICheckListItemRepository : IRepository<CheckListItem>
    {
        IEnumerable<CheckListItem> GetItemsByTenderId(int tenderId);
    }
}