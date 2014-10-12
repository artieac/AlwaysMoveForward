using System.Web.Security;
using ServiceStack.ServiceInterface;
using VP.Digital.Common.Entities;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// A controller for logging in a user
    /// </summary>
    [DefaultView("GrantAccess")]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// The post request with the user information
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A success view, or redirects to the login screen again</returns>
        public object Post(LoginRequest request)
        {
            string oauthToken = "foo";

            if (request != null)
            {
                oauthToken = request.OAuthToken;

                if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Password))
                {
                    // yes actually look up the user, but for now
                    DigitalUser digitalUser = this.ServiceManager.UserService.LogonUser(request.UserName, request.Password, this.Request.RemoteIp);

                    if (digitalUser != null)
                    {
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(digitalUser.Id.ToString(), false, 30);
                        string encryptedId = FormsAuthentication.Encrypt(authTicket);
                        Response.Cookies.AddSessionCookie(FormsAuthentication.FormsCookieName, encryptedId);
                        LoginResponse response = new LoginResponse();
                        response.OAuthToken = oauthToken;
                        return response;
                    }
                }
            }

            return this.Redirect(string.Format("/Authorization/Index?oauth_token={0}", oauthToken));
        }
    }
}
