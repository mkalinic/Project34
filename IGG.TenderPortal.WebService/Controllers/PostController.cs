using IGG.TenderPortal.WebService.Models;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;
using System;

namespace Tenderingportal.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet]
      //  [AuthorizationAFA(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public ActionResult GetPostsTo(int id, int? top=null)
        {
            List<Post> posts = GetDummyPosts();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, posts);

        }

        [HttpGet]
     //   [AuthorizationAFA(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public ActionResult GetPostsFrom(int id, int? top = null)
        {
            List<Post> posts = GetDummyPosts();
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, posts);

        }



        /// <summary>
        /// THIS IS UPLOADFILE ACCTUALY
        /// </summary>
        /// <param name="file"></param>
        /// <param name="postID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int? postID)
        {
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, new { name = "tempFileName", size = file.ContentLength });
        }

        private List<Post> GetDummyPosts()
        {
            return new List<Post>
            {
                GetDummyPost(),
                GetDummyPost(),
                GetDummyPost()
            };
        }

        private Post GetDummyPost()
        {
            return new Post
            {
                definition = "dasd",
                text = "dasdsad text afdsfd"
            };
        }
    }
}