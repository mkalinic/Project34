using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;
using System.Collections.Generic;
using System.Linq;

namespace IGG.TenderPortal.Data.Repositories
{
    public class TenderFileBlockRepository : RepositoryBase<TenderFileBlock>, ITenderFileBlockRepository
    {
        public TenderFileBlockRepository(IDbFactory dbFactory) 
            : base(dbFactory)
        {
        }

        public TenderFileBlock GetById(int id, int tenderId)
        {
            return DbContext
                .TenderFileBlock
                .Include("Tender")
                .Where(tf => tf.TenderFileBlockId == id)
                .SingleOrDefault(e => e.Tender.TenderId == tenderId);            
        }

        public IEnumerable<TenderFileBlock> GetByTenderId(int tenderId)
        {
            return DbContext
                .TenderFileBlock
                .Where(e => e.Tender.TenderId == tenderId);
        }
    }

    public interface ITenderFileBlockRepository : IRepository<TenderFileBlock>
    {
        IEnumerable<TenderFileBlock> GetByTenderId(int tenderId);
        TenderFileBlock GetById(int id, int tenderId);
    }
}
