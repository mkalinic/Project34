using AutoMapper;
using IGG.TenderPortal.Common;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;
using IGG.TenderPortal.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;

namespace Tenderingportal.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUserTenderService _userTenderService;
        private readonly IUserService _userService;
        private readonly ITenderService _tenderService;

        public ProjectController(ITenderService tenderService, IUserTenderService userTenderService, IUserService userService)
        {
            _userTenderService = userTenderService;
            _userService = userService;
            _tenderService = tenderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTopN(int n)
        {
            var tenders = _tenderService.GetTenders()
                .Where(t => t.ViewOnHomepage)
                .OrderByDescending(t => t.TenderId)
                .Take(n);

            return ReturnProjects(tenders);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var tenders = _tenderService.GetTenders();
            return ReturnProjects(tenders);
        }

        [HttpGet]
        public ActionResult GetTopNForUser(int n, int userID)
        {
            var tenders = _userTenderService.GetUserTenders()
                .Where(ut => ut.User.UserId == userID)
                .Select(ut => ut.Tender)
                .OrderByDescending(t => t.TenderId)
                .Take(n);

            return ReturnProjects(tenders);
        }

        [HttpGet]
        public ActionResult GetTopNForFrontPage(int n)
        {
            var tenders = _tenderService.GetTenders()
                .Where(t => t.ViewOnHomepage)
                .OrderByDescending(t => t.TenderId)
                .Take(n);

            return ReturnProjects(tenders);
        }

        [HttpGet]
        public ActionResult GetByID(int ID)
        {            
            var tender = _tenderService.GetTenderById(ID);
            var project = Mapper.Map<Tender, Project>(tender);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, project);
        }

        [HttpPost]
        public ActionResult Save(Project proj)
        {
            Tender tender;

            if (proj.ID <= 0)
                tender = CreateTender(proj);
            else
                tender = UpdateTender(proj);

            var model = Mapper.Map<Tender, Project>(tender);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, model);
        }

        private Tender CreateTender(Project proj)
        {
            var tender = Mapper.Map<Project, Tender>(proj);

            _tenderService.CreateTender(tender);
            _tenderService.SaveTender();

            return tender;
        }

        private Tender UpdateTender(Project proj)
        {
            var tender = _tenderService.GetTenderById(proj.ID);

            Mapper.Map(proj, tender);

            _tenderService.UpdateTender(tender);
            _tenderService.SaveTender();

            return tender;
        }

        [HttpPost]
        public async Task<ActionResult> Close(Project proj)
        {
            var tender = _tenderService.GetTenderById(proj.ID);
            tender.Status = TenderStatus.CLOSE; 
            _tenderService.UpdateTender(tender);
            _tenderService.SaveTender();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, proj);
        }

        [HttpPost]
        public ActionResult Delete(Project proj)
        {
            var tender = _tenderService.GetTenderById(proj.ID);
            _tenderService.RemoveTender(tender);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, proj);
        }


        [HttpGet]
        public ActionResult Search(string keywords)
        {
            var tenders = _tenderService.GetTenders()
                .Where(t => t.ProjectName.Contains(keywords)
                    || t.Client.Contains(keywords)
                    || t.Description.Contains(keywords)
                    || t.Place.Contains(keywords))
                .OrderByDescending(t => t.TenderId);

            return ReturnProjects(tenders);
        }


        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int projID)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, new { name = "tempFileName", size = file.ContentLength });
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, int projID)
        {            
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, new { name = "tempFileName", size = file.ContentLength });
        }

        [HttpPost]
        public String DeleteFile(string file)
        {
            string targetPath = Server.MapPath("~/UPLOADED_FILES/documents/projects/" + file);
            System.IO.File.Delete(targetPath);
            return "FILE_DELETED_SUCESFULLY";
        }

        /// <summary>
        /// Sends notice to everyone who is on this project
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> SendNoticesToPeopleOnProject(int projectID, string language = "nl")
        {         
            return "EMAILS_SENT_SUCESFULLY";
        }

        
        [HttpPost]
        public async Task<Post> SendPost(Post newPost, Post postRepliedTo, int projectID, string language = "nl")
        {
            return newPost;
        }


        /// <summary>
        /// Sends reply to one user
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Post> SendPostResponse(Post newPost, Post postRepliedTo, int projectID, string language = "nl")
        {
            return newPost;
        }



        [HttpPost]
        public async Task<JsonResult> SendNewUserInProjectMail(UsersProject usersProject, int projectID, string language = "nl")
        {
            var user = _userService.GetUserById(usersProject.IDuser);
            if (user == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, user);

            var tender = _tenderService.GetTenderById(projectID);
            if (tender == null)
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, tender);

            var userTender = Mapper.Map<UsersProject, UserTender>(usersProject);

            userTender.User = user;
            userTender.Tender = tender;

            _userTenderService.CreateUserTender(userTender);
            _userTenderService.SaveUserTender();

            var model = Mapper.Map<UserTender, UsersProject>(userTender);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, model);
        }


        [HttpGet]
        public async Task<JsonResult> OpenVault(int projectID)
        {

            //-- creating empty list of anonymous class
            var list = new[] { new { User = string.Empty, File = string.Empty, FilePath = string.Empty } }.ToList();
            list.RemoveAll(x => x.User.Equals(string.Empty));
            

            list = list.OrderBy(o => o.User).ToList();


            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, list);

        }

        [HttpPost]
        public async Task<FileResult> DownloadAllFilesForVault(string path, string tokenEncripted, string userName, int projectID, string language = "nl")
        {

            byte[] filedata = System.IO.File.ReadAllBytes(path);
            string contentType = MimeMapping.GetMimeMapping(path);            
            return File(filedata, contentType);

        }

        [HttpPost]
       // [AuthorizationAFA(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public FileResult ZipAllForTextBlock(int textblockID, string tokenEncripted, string userName)
        {
            
            string toPath = Server.MapPath("~/UPLOADED_FILES_ZIP_TEMP/test.zip");
            byte[] filedata = System.IO.File.ReadAllBytes(toPath);
            string contentType = MimeMapping.GetMimeMapping(toPath);            
            return File(filedata, contentType);
        }


        [HttpPost]
        [AuthorizationAFA(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public ActionResult SaveProjectFile(ProjectFile projf)
        {

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, projf);
        }

        [HttpPost]
        [AuthorizationAFA(AllowedUserTypes = "IGG")]
        public ActionResult DeleteProjectFile(ProjectFile projf)
        {

            string filePath = Server.MapPath("~/UPLOADED_FILES/projects/" + projf.file);
            System.IO.File.Delete(filePath);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, projf);
        }

        private static ActionResult ReturnProjects(IEnumerable<Tender> tenders)
        {
            var projects = Mapper.Map<IEnumerable<Tender>, IEnumerable<Project>>(tenders);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, projects);
        }
    }
}