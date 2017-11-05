using System.Collections.Generic;
using IGG.TenderPortal.Data.Infrastructure;
using IGG.TenderPortal.Data.Repositories;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Service
{
    public interface IMessageService
    {
        IEnumerable<Message> GetMessages();
        Message GetMessage(int id);
        void CreateMessage(Message message);        
    }

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Message> GetMessages()
        {
            var messages = _messageRepository.GetMessages();
            return messages;
        }

        public Message GetMessage(int id)
        {
            var message = _messageRepository.GetMessageById(id);
            return message;
        }

        public void CreateMessage(Message message)
        {
            _messageRepository.Add(message);
            _unitOfWork.Commit();
        }
    }
}
