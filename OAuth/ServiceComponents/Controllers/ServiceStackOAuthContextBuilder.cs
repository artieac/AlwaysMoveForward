using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DD = DevDefined.OAuth.Framework;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// This class overrides the Default DevDefined OAuth context builder so it can be populated from Service Stacks request
    /// </summary>
    public class ServiceStackOAuthContextBuilder : DD.OAuthContextBuilder
    {
        /// <summary>
        /// Create the OAuthContext from Services Stacks request
        /// </summary>
        /// <param name="request">The service stack request object</param>
        /// <returns>A DevDefined OAuth context</returns>
        public DD.IOAuthContext FromHttpRequest(ServiceStack.ServiceHost.IHttpRequest request)
        {
            var context = new DD.OAuthContext
            {
                RawUri = CleanUri(new Uri(request.AbsoluteUri)),
                Cookies = CollectCookies(request),
                Headers = GetCleanedNameValueCollection(request.Headers),
                RequestMethod = request.HttpMethod,
                FormEncodedParameters = GetCleanedNameValueCollection(request.FormData),
                QueryParameters = GetCleanedNameValueCollection(request.QueryString),
            };

            StreamReader streamReader = new StreamReader(request.InputStream);
            string rawContent = streamReader.ReadToEnd();

            if (!string.IsNullOrEmpty(rawContent))
            {
                context.RawContent = System.Text.Encoding.Unicode.GetBytes(rawContent);
            }

            ParseAuthorizationHeader(request.Headers, context);

            return context;
        }

        /// <summary>
        /// Extracts the paramters from cookies
        /// </summary>
        /// <param name="request">The service stack request object</param>
        /// <returns>The cookies as Name/Value pairs</returns>
        protected virtual NameValueCollection CollectCookies(ServiceStack.ServiceHost.IHttpRequest request)
        {
            return CollectCookiesFromHeaderString(request.Headers["Set-Cookie"]);
        }
    }
}
