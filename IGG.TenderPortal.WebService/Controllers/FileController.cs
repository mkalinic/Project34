using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.WebService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;

namespace Tenderingportal.Controllers
{
    public class FileController : Controller
    {
        // GET: Download
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
    //  [AuthorizationAFA(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public FileResult Download(string path, string tokenEncripted, string userName, int? id )
        {
                 return null;
        }

        [HttpGet]
        [AuthorizationAFA(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public ActionResult Delete(string path)
        {
            string targetPath = Server.MapPath("~/" + path);
            System.IO.File.Delete(targetPath);
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, "FILE DELETED");
        }


        [HttpPost]
        public ActionResult UploadFrontImage(HttpPostedFileBase file)
        {
           




            string tempFileName = "";
            string path = "~/UPLOADED_IMAGES/frontImage/";
            string fullFile = Server.MapPath(path + file.FileName);
            string FullFilePath = "";

            //---- deleting previous images

            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(path));
            foreach (FileInfo filee in di.GetFiles())
            {
                filee.Delete();
            }

            //---- / deleting previous images

            string fileNameOnly = "IGGtenderportal";
            string extension = Path.GetExtension(file.FileName);
            FullFilePath = Server.MapPath(path + fileNameOnly + extension);
            string targetPath = FullFilePath;//  folder + "/" + fileName;
            file.SaveAs(targetPath);
            // return "FILE_UPLOADED_SUCESFULLY";
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, new { name = tempFileName, size = file.ContentLength });
        }

        public ActionResult GetFrontImage()
        {
            string path = "~/UPLOADED_IMAGES/frontImage/";
            string fullFile = Server.MapPath(path);
            DirectoryInfo d = new DirectoryInfo(fullFile); 
            FileInfo[] Files = d.GetFiles(); 
            
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, new { name = Files[0].Name });
        }

    }
}