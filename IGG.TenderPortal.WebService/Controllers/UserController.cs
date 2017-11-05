using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class UserController : ApiController
    {        
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {            
            _userService = userService;
        }

        // GET: api/User
        public IEnumerable<UserModel> Get()
        {
            var users = _userService.GetUsers();
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(users);
        }

        // GET: api/User/5
        public UserModel Get(int id)
        {
            var user = _userService.GetUserById(id);
            return Mapper.Map<User, UserModel>(user);
        }

        // POST: api/User
        public void Post([FromBody]UserModel value)
        {
            var user = Mapper.Map<UserModel, User>(value);

            _userService.CreateUser(user);
            _userService.SaveUser();
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]UserModel value)
        {
            var user = _userService.GetUserById(id);

            Mapper.Map(value, user);

            _userService.UpdateUser(user);
            _userService.SaveUser();
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
            var user = _userService.GetUserById(id);

            _userService.RemoveUser(user);
            _userService.SaveUser();
        }
    }
}
