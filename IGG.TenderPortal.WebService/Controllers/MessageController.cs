using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessageService _messageService;
        private readonly IUserTenderService _userTenderService;

        public MessageController(IMessageService messageService, IUserTenderService userTenderService)
        {
            _messageService = messageService;
            _userTenderService = userTenderService;
        }

        // GET: api/Message
        public IEnumerable<MessageModel> Get()
        {
            var messages = _messageService.GetMessages();
            return Mapper.Map<IEnumerable<Message>, IEnumerable<MessageModel>>(messages);
        }

        // POST: api/Message
        public void Post([FromBody]MessageModel value)
        {
            var userTender = _userTenderService.GetUserTenderByIds(value.UserId, value.TenderId);
            if (userTender == null)
                return;

            var message = Mapper.Map<MessageModel, Message>(value);
            message.UserTender = userTender;

            _messageService.CreateMessage(message);
        }
    }
}
