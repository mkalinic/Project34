using IGG.TenderPortal.DtoModel;
using IGG.TenderPortal.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Tenderingportal.Authorization
{
    /// <summary>
    /// Example:   [AuthorizationAFA(AllowedRoles = Role.Administrator+","+Role.Assistant)] will allow a user with Admin or Assistant role to proceed to method. For all others 403 error will be returned to client. 
    ///  All possible roles are defined in Tenderingportal.Authorization.Role.cs file. 
    ///  Authentication is also checked by adding this attribute (401 will be returned if not authenticated)
    /// </summary>
    public class AuthorizationAFA : ActionFilterAttribute
    {

        public string AllowedUserTypes { get; set; }
        public static string CurentUserName { get; private set; }
        public static string CurentUserRole { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var tokenEncripted = filterContext.HttpContext.Request.Headers["token"];
            var userName = filterContext.HttpContext.Request.Headers["userName"];
            var rolesList = AllowedUserTypes.Replace(" ", string.Empty).Split(',').ToList();

            if (rolesList.Contains("GUEST"))
            {
                if (userName == null || userName.Equals(string.Empty))
                {
                     CurentUserName = "GUEST";
                }
                else
                {
                    CurentUserName = userName;
                }
                   
            }
            else
            {
                try
                {
                    string token = Crypting.Decrypt(tokenEncripted);
                    string[] tokenArr = token.Split('|');
                    string tokenRole = tokenArr[1];
                    string tokenUsername = tokenArr[tokenArr.Length - 1];

                    if (!rolesList.Contains(tokenRole))
                    {
                        returnUnauthorized();
                    }

                    if (!tokenUsername.Equals(userName))
                    {
                        returnUnauthenticated();
                    }

                    CurentUserName = userName;

                }
                catch(Exception err) 
                {
                    // somebody messed with the token on client

                    returnUnauthenticated();
                }
            }

           // TODO: For greater security ask the database for users role
           // also possible to put token in the database (per user) and bunch other data

    }

        /// <summary>
        /// here us specified a way of returning an error
        /// not the user he cailms to be
        /// </summary>
        private void returnUnauthenticated()
        {
            CurentUserName = null;
            HttpContext.Current.Response.StatusCode = 401;
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //HttpContext.Current.ApplicationInstance.Response.End();
            //throw new UnauthorizedAccessException();
        }


        /// <summary>
        /// here us specified a way of returning an error
        /// user does not have rights to access this
        /// </summary>
        private void returnUnauthorized()
        {
            CurentUserName = null;
            HttpContext.Current.Response.StatusCode = 403;
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //HttpContext.Current.ApplicationInstance.Response.End();
           // throw new UnauthorizedAccessException();

        }
    }
}