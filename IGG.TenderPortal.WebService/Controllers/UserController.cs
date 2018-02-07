using IGG.TenderPortal.WebService.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class UserController : Controller
    {
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
            List<User> users = GetDummyUsers();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, users);
        }

        [HttpGet]        
        public ActionResult GetSorted(int page, int pagesize, string column, bool desc)
        {
            List<User> users = GetDummyUsers();

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
            User user = GetDummyUser();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, user);
        }

        [HttpPost]
        public ActionResult SaveMyAccount(Models.User user)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, user);            
        }

        [HttpPost]        
        public ActionResult Save(User user)
        {
            User newuser = GetDummyUser();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, newuser);
        }

        [HttpGet]        
        public ActionResult GetAll()
        {
            List<Models.User> list = GetDummyUsers();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, list);
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

        private static List<User> GetDummyUsers()
        {
            return new List<User>
            {
                GetDummyUser(),
                GetDummyUser(),
                GetDummyUser()

            };
        }

        private static User GetDummyUser()
        {
            return new User
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
    }
}