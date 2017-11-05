using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class UserTenderController : ApiController
    {        
        private readonly IUserTenderService _userTenderService;
        private readonly IUserService _userService;
        private readonly ITenderService _tenderService;

        public UserTenderController(IUserTenderService userTenderService, IUserService userService, ITenderService tenderService)
        {            
            _userTenderService = userTenderService;
            _userService = userService;
            _tenderService = tenderService;
        }

        // GET: api/UserTender
        public IEnumerable<UserTenderModel> Get()
        {
            var userTenders = _userTenderService.GetUserTenders();
            return Mapper.Map<IEnumerable<UserTender>, IEnumerable<UserTenderModel>>(userTenders);
        }

        // GET: api/UserTender/5
        public UserTenderModel Get(int id)
        {
            var userTender = _userTenderService.GetUserTenderById(id);
            return Mapper.Map<UserTender, UserTenderModel>(userTender);
        }

        // POST: api/UserTender
        public void Post([FromBody]UserTenderModel value)
        {
            var user = _userService.GetUserById(value.UserId);
            var tender = _tenderService.GetTenderById(value.TenderId);

            var userTender = Mapper.Map<UserTenderModel, UserTender>(value);

            userTender.User = user;
            userTender.Tender = tender;

            _userTenderService.CreateUserTender(userTender);
            _userTenderService.SaveUserTender();
        }

        // PUT: api/UserTender/5
        public void Put(int id, [FromBody]UserTenderModel value)
        {
            var userTender = _userTenderService.GetUserTenderById(id);
            var user = _userService.GetUserById(value.UserId);
            var tender = _tenderService.GetTenderById(value.TenderId);

            Mapper.Map(value, userTender);

            userTender.User = user;
            userTender.Tender = tender;

            _userTenderService.UpdateUserTender(userTender);
            _userTenderService.SaveUserTender();
        }

        // DELETE: api/UserTender/5
        public void Delete(int id)
        {
            var userTender = _userTenderService.GetUserTenderById(id);

            _userTenderService.RemoveUserTender(userTender);
            _userTenderService.SaveUserTender();
        }
    }
}
