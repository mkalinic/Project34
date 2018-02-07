using DatabaseCommunicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tenderingportal.Authorization;
using Tenderingportal.Models;

namespace Tenderingportal.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult Register(FormCollection data)
        {
            return View();
        }

        [HttpPost]
        public string Login(FormCollection data)
        {

            //-------------- here comes username and password check and writing token to database
            // u tokenu moze da stoji i broj koji pokazuje na kom mestu pocinje username
            string password = data["password"];
            string username = data["username"];

            User user = Models.User.GetByUsernameAndPassword(username, password);
            if ( user == null) return "invalid attempt";

 
        //--- then token creation
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var referer = HttpContext.Request.Headers["Referer"];
            string token = userAgent + "|"+ user.userType + "|" + referer + "|"+ user.username;// + username;
           // here put smart combination for token

            string tokenEncripted = Crypting.Encrypt(token);


            Logbook book = new Logbook();
            book.action = Logbook.ACTION_LOGGED_IN;
            book.userID = user.ID;
            book.time = DateTime.Now;
            book.projectID = null;
            book.filename = null;
            book.textbox = null;

            book.Save();


            return tokenEncripted;
        }
        // Logut is done simply by removing the token from web-storage on the client side, nothing to be done here

        [AuthorizationAFA(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public ActionResult GetTranslations(string lang_code)
        {
            var ooo = Database.Query("select text, translation from Translations where lang_code = '"+ lang_code+"'");
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, ooo);
        }

    }
}