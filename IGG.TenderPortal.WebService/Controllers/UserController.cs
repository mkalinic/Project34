using IGG.TenderPortal.Service;
using IGG.TenderPortal.WebService.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System;
using AutoMapper;
using IGG.TenderPortal.Model;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;

        private readonly IUserTenderService _userTenderService;
        private readonly IUserService _userService;
        private readonly ITenderService _tenderService;

        public UserController(ITenderService tenderService, IUserTenderService userTenderService, IUserService userService)
        {
            _userTenderService = userTenderService;
            _userService = userService;
            _tenderService = tenderService;
        }

        [HttpGet]        
        public ActionResult GetById(int id)
        {            
            var user = _userService.GetUserById(id);            
            var userModel = Mapper.Map<Model.User, Models.User>(user);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userModel);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string username)
        {           
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, new { name = "tempFileName", size = file.ContentLength });
        }

        [HttpGet]
        public ActionResult Get(int page, int pagesize)
        {
            var users = _userService.GetUsers()
                .Skip((page - 1) * pagesize)
                .Take(pagesize);
            var modelUsers = Mapper.Map<IEnumerable<Model.User>, IEnumerable<Models.User>>(users);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, modelUsers);
        }

        [HttpGet]        
        public ActionResult GetSorted(int page, int pagesize, string column, bool desc)
        {
            //TODO: implement order by column and desc
            var users = _userService.GetUsers().OrderByDescending( x => x.Name)
                .Skip((page - 1) * pagesize)
                .Take(pagesize);

            var modelUsers = Mapper.Map<IEnumerable<Model.User>, IEnumerable<Models.User>>(users);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, modelUsers);
        }

        [HttpGet]
        public ActionResult Delete(int userId)
        {
            var user = _userService.GetUserById(userId);

            _userService.RemoveUser(user);
            _userService.SaveUser();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userId);
        }

        [HttpGet]
        public ActionResult GetByIdWithPass(int id)
        {
            //TODO
            var user = _userService.GetUserById(id);
            var userModel = Mapper.Map<Model.User, Models.User>(user);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userModel);
        }

        [HttpGet]
        public ActionResult GetMyAccount()
        {
            //TODO
            //var userId = User.Identity.GetUserId();
            //var applicationUser = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();

            var user = _userService.GetUserById(1);
            var modelUser =  Mapper.Map<Model.User, Models.User>(user);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, modelUser);
        }

        [HttpPost]
        public ActionResult SaveMyAccount(Models.User value)
        {
            //TODO
            if (value.ID <= 0)
                CreateUser(value);
            else
                UpdateUser(value);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, value);            
        }

        [HttpPost]        
        public ActionResult Save(Models.User value)
        {
            if(value.ID <= 0)
                CreateUser(value);
            else
                UpdateUser(value);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, value);
        }

        private void CreateUser(Models.User value)
        {
            var user = Mapper.Map<Models.User, Model.User>(value);

            _userService.CreateUser(user);
            _userService.SaveUser();
        }

        private void UpdateUser(Models.User value)
        {
            var user = _userService.GetUserById(value.ID);

            Mapper.Map(value, user);

            _userService.UpdateUser(user);
            _userService.SaveUser();
        }

        [HttpGet]        
        public ActionResult GetAll()
        {
            var users = _userService.GetUsers();
            var userModels = Mapper.Map<IEnumerable<Model.User>, IEnumerable<Models.User>>(users);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userModels);
        }

        [HttpPost]        
        public ActionResult SaveAll(List<Models.User> list)
        {
            foreach(var userModel in list)
            {
                UpdateUser(userModel);
            };

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, list);
        }

        [HttpGet]
        // [AuthorizationAFA(AllowedUserTypes = "GUEST")]
        public async System.Threading.Tasks.Task<ActionResult> ForgottenPassword(string usernameOrEmail, string language = "nl")
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }

        [HttpGet]
        // [AuthorizationAFA(AllowedUserTypes = "GUEST")]
        public ActionResult UpdatePassword(string token)
        {
            var user1 = GetDummyUser();            
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, user1);

        }

        [HttpPost]        
        public ActionResult SaveNewPassword(Models.User user, string token)
        {           
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "OK");
        }

        public ActionResult DeleteUserProject(Models.UsersProject userProject)
        {
            var userTender = _userTenderService.GetUserTenderById(userProject.ID);

            _userTenderService.RemoveUserTender(userTender);
            _userTenderService.SaveUserTender();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userProject);
        }
        
        public ActionResult SaveUserProject(Models.UsersProject value)
        {
            var user = _userService.GetUserById(value.IDuser);
            var tender = _tenderService.GetTenderById(value.IDproject);

            var userTender = Mapper.Map<UsersProject, UserTender>(value);

            userTender.User = user;
            userTender.Tender = tender;

            _userTenderService.CreateUserTender(userTender);
            _userTenderService.SaveUserTender();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, value);
        }

        public ActionResult AddIggerToProject(int IggerID, int ProjectID)
        {
            var user = _userService.GetUserById(IggerID);
            var tender = _tenderService.GetTenderById(ProjectID);

            var userTender = new UserTender {
                UserType = Common.ClientType.IGG
            };

            userTender.User = user;
            userTender.Tender = tender;

            _userTenderService.CreateUserTender(userTender);
            _userTenderService.SaveUserTender();

            UsersProject userProject = Mapper.Map<UserTender, UsersProject>(userTender);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userProject);
        }
        
        public async System.Threading.Tasks.Task<ActionResult> SendCredentials(Models.User user, string language = "nl")
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, user);
        }

        public ActionResult GetCount()
        {
            var count = _userService.GetUsers().Count();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, count);
        }

        public ActionResult Search(string keyword)
        {
            var users = _userService.GetUsers()
                .Where(t => t.Name.Contains(keyword)
                    || t.Surname.Contains(keyword)
                    || t.CompanyName.Contains(keyword))
                .OrderByDescending(t => t.UserId);

            var projects = Mapper.Map<IEnumerable<Model.User>, IEnumerable<Models.User>>(users);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, GetDummyUser());
        }                

        private static Models.User GetDummyUser()
        {
            return new Models.User
            {
                ID = 1,
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
    }
}