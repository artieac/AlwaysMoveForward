using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Moq;
using System.Web;
using ServiceStack.ServiceInterface;

namespace VP.Digital.Security.OAuth.UnitTests.Mock.Controllers
{
    public class MockServiceStackRequestContext : ServiceStack.ServiceHost.IRequestContext
    {
        public MockServiceStackRequestContext(string absoluteUri, string oauthHeader)
        {
            this.AbsoluteUri = absoluteUri;
            this.HeaderValues.Add("Authorization", oauthHeader);
        }

        Mock<ServiceStack.ServiceHost.IHttpRequest> mockHttpRequest;

        System.Collections.Specialized.NameValueCollection headerValues = null;

        private System.Collections.Specialized.NameValueCollection HeaderValues
        {
            get
            {
                if (this.headerValues == null)
                {
                    this.headerValues = new System.Collections.Specialized.NameValueCollection();
                }

                return this.headerValues;
            }
        }

        public string AbsoluteUri { get; set; }

        public string CompressionType
        {
            get { throw new NotImplementedException(); }
        }

        public string ContentType
        {
            get { return "text/html"; }
        }

        public IDictionary<string, System.Net.Cookie> Cookies
        {
            get { return new Dictionary<string, System.Net.Cookie>(); }
        }

        public ServiceStack.ServiceHost.EndpointAttributes EndpointAttributes
        {
            get { throw new NotImplementedException(); }
        }

        public ServiceStack.ServiceHost.IFile[] Files
        {
            get { return null; }
        }

        public T Get<T>() where T : class
        {
            T retVal = null;

            if (typeof(T) == typeof(ServiceStack.ServiceHost.IHttpRequest))
            {
                if (this.mockHttpRequest == null)
                {
                    this.mockHttpRequest = new Mock<ServiceStack.ServiceHost.IHttpRequest>();
                    this.mockHttpRequest.SetupGet(x => x.Headers).Returns(this.HeaderValues);
                    this.mockHttpRequest.SetupGet(x => x.AbsoluteUri).Returns(this.AbsoluteUri);
                    this.mockHttpRequest.SetupGet(x => x.HttpMethod).Returns("GET");
                    this.mockHttpRequest.SetupGet(x => x.FormData).Returns(new System.Collections.Specialized.NameValueCollection());
                    this.mockHttpRequest.SetupGet(x => x.QueryString).Returns(new System.Collections.Specialized.NameValueCollection());
                    this.mockHttpRequest.SetupGet(x => x.InputStream).Returns(new MemoryStream());
                    retVal = this.mockHttpRequest.Object as T;
                }
            }

            return retVal;
        }

        public string GetHeader(string headerName)
        {
            return this.HeaderValues[headerName];
        }

        public string IpAddress
        {
            get { return "127.0.0.1"; }
        }

        public string PathInfo
        {
            get { return string.Empty; }
        }

        public ServiceStack.ServiceHost.IRequestAttributes RequestAttributes
        {
            get { throw new NotImplementedException(); }
        }

        public string ResponseContentType
        {
            get { return string.Empty; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
