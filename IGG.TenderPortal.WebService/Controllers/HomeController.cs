using IGG.TenderPortal.WebService.Compression;
using IGG.TenderPortal.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;
using IGG.TenderPortal.WebService.Models;

namespace IGG.TenderPortal.WebService.Controllers
{
    public class HomeController : Controller
    {
 


        // GET: Home
        public ActionResult Index()
        {

            var ooo = GetDummyUsers();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, ooo);
        }

        [HttpGet]
        public ActionResult GetFrontPageTitle(string lang)
        {           
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, new { title= "FrontPageTitle", text = "FrontPageText" });
        }

        [HttpPost]
        public ActionResult PostAnything(FormCollection data)
        {
            var ooo = GetDummyUsers();

            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, ooo);
        }


        [HttpPost]
        public String UploadFile(HttpPostedFileBase file)
        {
            string targetPath = Server.MapPath("~/UPLOADED_FILES/" + file.FileName);
            file.SaveAs(targetPath);
            return "FILE_UPLOADED_SUCESFULLY";
        }

        [HttpGet]
        public FileResult DownloadZip()
        {
            //-- compress:
            string fromPath = Server.MapPath("~/UPLOADED_FILES/");
            string toPath = Server.MapPath("~/UPLOADED_FILES_ZIP_TEMP/All.zip");
            System.IO.File.Delete(toPath);
            Zip.ZipFolder(fromPath, toPath);

            //-- download:
            byte[] filedata = System.IO.File.ReadAllBytes(toPath);
            string contentType = MimeMapping.GetMimeMapping(toPath);
            System.IO.File.Delete(toPath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "All.zip",
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
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