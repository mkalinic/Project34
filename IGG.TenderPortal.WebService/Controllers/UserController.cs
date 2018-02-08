using IGG.TenderPortal.Service;
using IGG.TenderPortal.WebService.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System;
using AutoMapper;
using IGG.TenderPortal.Model;
using Microsoft.AspNet.Identity;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private ApplicationUserManager _userManager;

        public UserController(IUserService userService, ApplicationUserManager userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]        
        public ActionResult GetById(int id)
        {
            Models.User user = GetDummyUser();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, user);

        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string username)
        {           
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, new { name = "tempFileName", size = file.ContentLength });
        }

        [HttpGet]
        public ActionResult Get(int page, int pagesize)
        {
            List<Models.User> users = GetDummyUsers();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, users);
        }

        [HttpGet]        
        public ActionResult GetSorted(int page, int pagesize, string column, bool desc)
        {
            List<Models.User> users = GetDummyUsers();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, users);

        }

        [HttpGet]
        public ActionResult Delete(int UserID)
        {            
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, UserID);
        }

        [HttpGet]
        public ActionResult GetByIdWithPass(int id)
        {
            var user = GetDummyUser();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, user);
        }

        [HttpGet]
        public ActionResult GetMyAccount()
        {
            //var userId = User.Identity.GetUserId();
            //var applicationUser = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();

            var user = _userService.GetUserById(1);
            var modelUser =  Mapper.Map<Model.User, Models.User>(user);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, modelUser);
        }

        [HttpPost]
        public ActionResult SaveMyAccount(Models.User value)
        {
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
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userProject);
        }
        
        public ActionResult SaveUserProject(Models.UsersProject userProject)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userProject);
        }

        public ActionResult AddIggerToProject(int IggerID, int ProjectID)
        {
            UsersProject userProject = new UsersProject();
            userProject.IDproject = ProjectID;
            userProject.IDuser = IggerID;
            userProject.userType = "IGG";
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, userProject);
        }
        
        public async System.Threading.Tasks.Task<ActionResult> SendCredentials(Models.User user, string language = "nl")
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, user);
        }

        public ActionResult GetCount()
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, 10);
        }

        public ActionResult Search(string keyword)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, GetDummyUser());
        }        

        private static List<Models.User> GetDummyUsers()
        {
            return new List<Models.User>
            {
                GetDummyUser(),
                GetDummyUser(),
                GetDummyUser()

            };
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