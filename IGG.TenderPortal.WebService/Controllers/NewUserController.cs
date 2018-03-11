﻿using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;
using Microsoft.AspNet.Identity;
using System.Web;

namespace IGG.TenderPortal.WebService.Controllers
{
    //[Authorize]
    public class NewUserController : ApiController
    {        
        private readonly IUserService _userService;

        public NewUserController(IUserService userService)
        {            
            _userService = userService;
        }

        [HttpGet]
        [Route("api/User/GetMyAccount")]
        public IGG.TenderPortal.DtoModel.User GetMyAccount()
        {
            return new IGG.TenderPortal.DtoModel.User
            {
                address = "address1",
                city = "city1",
                companyName = "companyName1",
                country = "CH",
                email = "test1@gmail.com",
                name = "Test1",
                username = "test1@gmail.com",
                userType = "IGG"                
            };
        } 

        // GET: api/User
        public IEnumerable<UserModel> Get()
        {
            var users = _userService.GetUsers();
            return Mapper.Map<IEnumerable<Model.User>, IEnumerable<UserModel>>(users);
        }

        // GET: api/User/5
        public UserModel Get(int id)
        {
            var test1 = HttpContext.Current.User.Identity.IsAuthenticated;
            var test2 = User.Identity.GetUserId();
            var user = _userService.GetUserById(id);
            return Mapper.Map<Model.User, UserModel>(user);
        }

        // POST: api/User
        public void Post([FromBody]UserModel value)
        {
            var user = Mapper.Map<UserModel, Model.User>(value);

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
