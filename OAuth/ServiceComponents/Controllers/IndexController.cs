using ServiceStack.ServiceInterface;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;

namespace VP.Digital.Security.OAuth.ServiceComponents.Controllers
{
    /// <summary>
    /// The inital page for logging in an OAuth user
    /// </summary>
    [DefaultView("Index")]
    public class IndexController : ControllerBase
    {
        /// <summary>
        /// Handle the get request
        /// </summary>
        /// <param name="request">The parsed incoming parameters</param>
        /// <returns>The response, which is returned as a model to the DefaultView</returns>
        public object Get(IndexRequest request)
        {
            return new IndexResponse { Token = request.oauth_token };
        }
    }
}
