using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using VP.Digital.Common.Utilities.Encryption;
using VP.Digital.Common.Utilities.Logging;
using VP.Digital.Security.OAuth.Contracts;
using VP.Digital.Security.OAuth.Common;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.ServiceComponents.Controllers;

namespace VP.Digital.Security.OAuth.WebService.Controllers
{
    [FallbackRoute("/{Path*}")]
    public class FallbackRequest : IRequiresRequestStream
    {
        public Stream RequestStream { get; set; }
    }

    public class FallbackProxy : ControllerBase
    {
        /// <summary>
        /// Pull out the proxy target form the url, it will be the first element in the query path.
        /// </summary>
        /// <param name="path">The query path</param>
        /// <returns>The proxy target host</returns>
        private string ExtractTargetServiceFromPath(string path)
        {
            string retVal = string.Empty;

            string[] splitPath = path.Split('/');

            if (splitPath.Length > 1)
            {
                retVal = splitPath[1];
            }

            return retVal;
        }       

        public object Any(FallbackRequest fallback)
        {
            ValidatedToken validatedToken = null;

            try
            {
                // Note we could instead use the oauth context and call AccessProtectedResource on a provider, but 
                // that will result in two database access calls at least (1 for the DevDefined processing, and 1 for our lookup of the validated token
                // This way we just do one.
                IOAuthContext oauthContext = new ServiceStackOAuthContextBuilder().FromHttpRequest(Request);
                validatedToken = this.ServiceManager.TokenService.ValidateSignature(oauthContext, oauthContext.Token, VP.Digital.Security.OAuth.Contracts.Constants.HmacSha1SignatureMethod, oauthContext.GenerateSignatureBase());
            }
            catch (Exception e)
            {
                LogManager.GetLogger(this.GetType()).Error(e);
                validatedToken = null;
            }

            if (validatedToken != null && validatedToken.User != null && validatedToken.OAuthToken != null)
            {
                OAuthSecurityPrincipal  principal = new OAuthSecurityPrincipal(validatedToken.User, validatedToken.OAuthToken);
                System.Threading.Thread.CurrentPrincipal = principal;

                string proxyTargetHost = string.Empty;

                String targetService = this.ExtractTargetServiceFromPath(this.Request.PathInfo);
                try
                {
                    proxyTargetHost = System.Configuration.ConfigurationManager.AppSettings[targetService];
                }
                catch (Exception e)
                {
                    LogManager.GetLogger(this.GetType()).Error(e);
                }

                string retVal = string.Empty;

                if (!string.IsNullOrEmpty(proxyTargetHost))
                {
                    return
                        new HttpResult(
                            this.GetResponse(
                                this.Request,
                                this.BuildWebRequest(this.Request, proxyTargetHost, fallback.RequestStream),
                                this.Response),
                            this.Response.ContentType);
                }
                else
                {
                    return new HttpResult(HttpStatusCode.NotFound, "Resource not found.");
                }
            }
            else
            {
                return new HttpResult(HttpStatusCode.Unauthorized);
            }
        }

        private byte[] GetResponse(IHttpRequest proxyRequest, HttpWebRequest request, IHttpResponse proxyResponse)
        {
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                byte[] responseData = this.GetResponseStreamBytes(response);

                proxyResponse.ContentType = response.ContentType;
                foreach (Cookie receivedCookie in response.Cookies)
                {
                    Cookie c = new Cookie(receivedCookie.Name,
                                       receivedCookie.Value);
                    c.Domain = proxyRequest.Headers["Host"];
                    c.Expires = receivedCookie.Expires;
                    c.HttpOnly = receivedCookie.HttpOnly;
                    c.Path = receivedCookie.Path;
                    c.Secure = receivedCookie.Secure;
                    proxyResponse.Cookies.AddCookie(c);
                }
                return responseData;
            }
            catch (System.Net.WebException)
            {
                throw new HttpError(HttpStatusCode.NotFound, "Not Found");
            }
        }

        private HttpWebRequest BuildWebRequest(IHttpRequest inboundRequest, string destinationAuthority, Stream inboundStream)
        {
            var destinationUrl = destinationAuthority + this.Request.RawUrl;

            HttpWebRequest outboundRequest = (HttpWebRequest)HttpWebRequest.Create(destinationUrl);
            outboundRequest.Method = inboundRequest.HttpMethod;
            outboundRequest.UserAgent = inboundRequest.UserAgent;
            outboundRequest.KeepAlive = true;
            outboundRequest.Headers.Add("X-Forwarded-For", inboundRequest.UserHostAddress + "," + inboundRequest.GetUrlHostName());
            
            var cookieContainer = new CookieContainer();

            foreach (var cookieKVP in inboundRequest.Cookies)
            {
                var cookie = cookieKVP.Value;
                Cookie newCookie = new Cookie(cookie.Name, cookie.Value);
                newCookie.Domain = new Uri(destinationUrl).Host;
                newCookie.Expires = cookie.Expires;
                newCookie.HttpOnly = cookie.HttpOnly;
                newCookie.Path = cookie.Path;
                newCookie.Secure = cookie.Secure;
                cookieContainer.Add(cookie);
            }

            UserTransferManager userTransferManager = new UserTransferManager();
            Cookie encryptedCookie = new Cookie(VP.Digital.Security.OAuth.Contracts.Constants.ProxyUserCookieName, userTransferManager.Encrypt(this.CurrentPrincipal.User));
            encryptedCookie.Domain = new Uri(destinationUrl).Host;
            cookieContainer.Add(encryptedCookie);

            outboundRequest.CookieContainer = cookieContainer;

            if (inboundRequest.ContentLength > 0 || inboundRequest.Headers.Get("Transfer-Encoding") != null)
            {
                outboundRequest.ContentType = inboundRequest.ContentType;
                outboundRequest.ContentLength = 0;

                string rawContent = inboundRequest.GetRawBody();
                
                if (!string.IsNullOrEmpty(rawContent))
                {
                    inboundStream.Position = 0;
                    Stream clientStream = inboundStream;
                    byte[] clientPostData = new byte[inboundRequest.InputStream.Length];
                    clientStream.Read(clientPostData, 0, (int)inboundRequest.InputStream.Length);

                    outboundRequest.ContentLength = clientPostData.Length;

                    Stream stream = outboundRequest.GetRequestStream();
                    stream.Write(clientPostData, 0, clientPostData.Length);
                    stream.Close();
                    clientStream.Close();
                    stream.Dispose();
                    clientStream.Dispose();
                }
            }

            return outboundRequest;
        }

        public byte[] GetResponseStreamBytes(HttpWebResponse response)
        {
            int bufferSize = 256;
            byte[] buffer = new byte[bufferSize];
            Stream responseStream;
            MemoryStream memoryStream = new MemoryStream();
            int remoteResponseCount;
            byte[] responseData;

            responseStream = response.GetResponseStream();
            remoteResponseCount = responseStream.Read(buffer, 0, bufferSize);

            while (remoteResponseCount > 0)
            {
                memoryStream.Write(buffer, 0, remoteResponseCount);
                remoteResponseCount = responseStream.Read(buffer, 0, bufferSize);
            }

            responseData = memoryStream.ToArray();

            memoryStream.Close();
            responseStream.Close();

            memoryStream.Dispose();
            responseStream.Dispose();

            return responseData;
        }
    }
}
