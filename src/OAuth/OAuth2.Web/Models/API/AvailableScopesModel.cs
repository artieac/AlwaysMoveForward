using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlwaysMoveForward.OAuth2.Web.Models.API
{
    public class AvailableScopesModel
    {
        public IList<ProtectedApiScope> ApiScopes { get; set; }
        public IList<string> IdentityScopes { get; set; }
    }
}
