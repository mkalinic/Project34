using AutoMapper;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.Service;
using IGG.TenderPortal.WebService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;

namespace Tenderingportal.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ITenderService _tenderService;

        public ProjectController(ITenderService tenderService)
        {
            _tenderService = tenderService;
        }

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTopN(int n)
        {


            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, GetDummyProjects());

        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var tenders = _tenderService.GetTenders();
            var projects = Mapper.Map<IEnumerable<Tender>, IEnumerable<Project>>(tenders);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, projects);
        }

        [HttpGet]
        public ActionResult GetTopNForUser(int n, int userID)
        {

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, GetDummyProjects());

        }

        [HttpGet]
        public ActionResult GetTopNForFrontPage(int n)
        {           
            return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, GetDummyProjects());

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
            if(proj.ID <= 0)
                CreateTender(proj);
            else
                UpdateTender(proj);

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, proj);
        }

        private void CreateTender(Project proj)
        {
            var tender = Mapper.Map<Project, Tender>(proj);

            _tenderService.CreateTender(tender);
            _tenderService.SaveTender();
        }

        private void UpdateTender(Project proj)
        {
            var tender = _tenderService.GetTenderById(proj.ID);

            Mapper.Map(proj, tender);

            _tenderService.UpdateTender(tender);
            _tenderService.SaveTender();
        }

        [HttpPost]
        public async Task<ActionResult> Close(Project proj)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, proj);
        }

        [HttpPost]
        public ActionResult Delete(Project proj)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, proj);
        }


        [HttpGet]
        public ActionResult Search(String keywords)
        {
            List<Project> projects = GetDummyProjects();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, projects);
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
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, usersProject);
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

        private List<Project> GetDummyProjects()
        {
            return new List<Project>
            {
                GetDummyProject(),
            };
        }

        private Project GetDummyProject()
        {
            return new Project
            {
                name = "Dummy",
                status = "Status"
            };
        }
    }
}