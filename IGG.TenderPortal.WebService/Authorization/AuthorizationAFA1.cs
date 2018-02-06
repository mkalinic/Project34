using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;


namespace Tenderingportal.Authorization
{
    /// <summary>
    /// Example:   [AuthorizationAFA(AllowedRoles = Role.Administrator+","+Role.Assistant)] will allow a user with Admin or Assistant role to proceed to method. For all others 403 error will be returned to client. 
    ///  All possible roles are defined in Tenderingportal.Authorization.Role.cs file. 
    ///  Authentication is also checked by adding this attribute (401 will be returned if not authenticated)
    /// </summary>
    public class AuthorizationAFA1 : AuthorizeAttribute    
    {
        public string AllowedUserTypes { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.GetValues("Token");
            if (token == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}