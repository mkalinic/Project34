using System;
using System.Collections.Generic;
using System.Linq;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Repositories
{
    public class MilestoneRepository : RepositoryBase<Milestone>, IMilestoneRepository
    {
        public MilestoneRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Milestone GetById(int id, int tenderId)
        {
            return DbContext
                .Milestone
                .Include("Tender")
                .SingleOrDefault(e => e.MilestoneId == id 
                    && e.Tender.TenderId == tenderId);
        }

        public IEnumerable<Milestone> GetByTenderId(int tenderId)
        {
            return DbContext
                .Milestone
                .Where(e => e.Tender.TenderId == tenderId);
        }
    }

    public interface IMilestoneRepository : IRepository<Milestone>
    {
        IEnumerable<Milestone> GetByTenderId(int id);
        Milestone GetById(int id, int tenderId);
    }
}