using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using System.Threading.Tasks;
using IGG.TenderPortal.Model.Identity;
using Microsoft.AspNet.Identity;
using IGG.TenderPortal.Data;
using IGG.TenderPortal.WebService.Models;
using Tenderingportal.Authorization;

namespace IGG.TenderPortal.WebService.Controllers
{
    [RoutePrefix("api/Account")]
    public class NewAccountController : ApiController
    {
        private ApplicationUserManager _userManager;        

        public NewAccountController(ApplicationUserManager userManager)
        {
            _userManager = userManager;            
        }

        //public AccountController()
        //{
        //    var test1 = 1;
        //}

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //POST api/Account/Register
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(AppUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, "Admin123.");

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [Route("Login")]
        [HttpPost]
        public string Login(CredentialsModel data)
        {

            //-------------- here comes username and password check and writing token to database
            // u tokenu moze da stoji i broj koji pokazuje na kom mestu pocinje username
            string password = data.password;
            string username = data.username;

            ApplicationUser user = UserManager.Find(username, password);
            if (user == null) return "invalid attempt";


            //--- then token creation
            var userAgent = Request.Headers.GetValues("User-Agent");
            //var referer = Request.Headers.GetValues("Referer");
            var referer = "Referer";
            string token = userAgent + "|" + "IGG" + "|" + referer + "|" + user.UserName;// + username;
                                                                                                 // here put smart combination for token

            string tokenEncripted = Crypting.Encrypt(token);


            return tokenEncripted;
        }

        [AuthorizationAFA1(AllowedUserTypes = "IGG,CONSULTANT,CANDIDATE,CLIENT,TENDER-TEAM")]
        public string GetTranslations()
        {
            var ooo = "Test token";
            //return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, ooo);
            return ooo;
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
