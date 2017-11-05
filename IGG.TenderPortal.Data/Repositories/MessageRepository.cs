using System.Collections.Generic;
using System.Linq;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Repositories
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<Message> GetMessages()
        {
           return DbContext
                .Message
                .Include("UserTender")
                .Include("UserTender.User")
                .Include("UserTender.Tender")
                .ToList();
        }

        public Message GetMessageById(int id)
        {
            return DbContext
                .Message
                .Include("UserTender")
                .SingleOrDefault(e => e.MessageId == id);
        }
    }

    public interface IMessageRepository : IRepository<Message>
    {
        IEnumerable<Message> GetMessages();
        Message GetMessageById(int id);
    }
}