using System.Collections.Generic;
using System.Linq;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Repositories
{
    public class TenderRepository : RepositoryBase<Tender>, ITenderRepository
    {
        public TenderRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<Tender> GetTenders()
        {
            return DbContext
             .Tender             
             .ToList();
        }

        public Tender GetTenderById(int tenderId)
        {
            return DbContext
                .Tender
                .SingleOrDefault(e => e.TenderId == tenderId);
        }
    }

    public interface ITenderRepository : IRepository<Tender>
    {
        IEnumerable<Tender> GetTenders();
        Tender GetTenderById(int tenderId);
    }
}
