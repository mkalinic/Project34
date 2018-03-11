using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IGG.TenderPortal.WebService;
using IGG.TenderPortal.Data;
using IGG.TenderPortal.Service;
using AutoMapper;
using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.Model;
using IGG.TenderPortal.WebService.Controllers;
using IGG.TenderPortal.Model.Identity;
using IGG.TenderPortal.WebService.Models;

namespace WebApplicationTemplateForMvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUserService _userService;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUserService userService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _userService = userService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // The Authorize Action is the end point which gets called when you access any
        // protected Web API. If the user is not logged in then they will be redirected to 
        // the Login page. After a successful login you can call a Web API.
        [HttpGet]
        public ActionResult Authorize()
        {
            var claims = new ClaimsPrincipal(User).Claims.ToArray();
            var identity = new ClaimsIdentity(claims, "Bearer");
            AuthenticationManager.SignIn(identity);
            return new EmptyResult();
        }
        
        // GET: /Account/GetHometown
        public async Task<ActionResult> GetHometown()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, user.Hometown);
        }     

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(CredentialsModel model)
        {
            if (!ModelState.IsValid)
            {
                return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, "invalid data");
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.username, model.password, model.rememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, GetToken(model.username));
                case SignInStatus.LockedOut:
                    return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, "Lockout");
                case SignInStatus.RequiresVerification:
                    return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, "ToBeVerifed");
                case SignInStatus.Failure:
                default:
                    return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, "Invalid login attempt.");
            }
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {            
            IdentityResult result = new IdentityResult();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Hometown = model.Hometown };
                result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var userModel = new IGG.TenderPortal.DtoModel.User {
                        email = model.Email,
                        city = model.Hometown,
                    };

                    CreateUser(userModel, user.Id);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return JsonResponse.GetJsonResult(JsonResponse.OK_DATA_RESPONSE, GetToken(user.UserName));
                }
                AddErrors(result);
            }

            return JsonResponse.GetJsonResult(JsonResponse.ERROR_RESPONSE, result.Errors);
        }
        
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private void CreateUser(IGG.TenderPortal.DtoModel.User model, string userId)
        {
            var user = Mapper.Map<IGG.TenderPortal.DtoModel.User, IGG.TenderPortal.Model.User>(model);

            user.Guid = userId;
            _userService.CreateUser(user);
            _userService.SaveUser();
        }

        private string GetToken(string username)
        {
            var userAgent = Request.Headers.GetValues("User-Agent");
            //var referer = Request.Headers.GetValues("Referer");
            var referer = "Referer";
            string token = userAgent + "|" + "IGG" + "|" + referer + "|" + username;// + username;                                                                                         
            string tokenEncripted = Crypting.Encrypt(token);
            return tokenEncripted;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        
        #endregion
    }
}
