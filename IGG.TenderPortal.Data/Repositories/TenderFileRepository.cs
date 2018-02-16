using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;
using System.Collections.Generic;
using System.Linq;

namespace IGG.TenderPortal.Data.Repositories
{
    public class TenderFileRepository : RepositoryBase<TenderFile>, ITenderFileRepository
    {
        public TenderFileRepository(IDbFactory dbFactory) 
            : base(dbFactory)
        {
        }

        public IEnumerable<TenderFile> GetByBlockId(int blockId)
        {
            return DbContext
            .TenderFile
            .Where(e => e.TenderFileBlock.TenderFileBlockId == blockId);
        }

        public TenderFile GetById(int id, int blockId)
        {
            return DbContext
                .TenderFile
                .Include("TenderFileBlock")
                .Where(tf => tf.TenderFileId == id)
                .SingleOrDefault(e => e.TenderFileBlock.TenderFileBlockId == blockId);
        }
    }

    public interface ITenderFileRepository : IRepository<TenderFile>
    {
        IEnumerable<TenderFile> GetByBlockId(int blockId);
        TenderFile GetById(int id, int blockId);
    }
}
