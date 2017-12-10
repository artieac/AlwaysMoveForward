using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel
{
    public class ProtectedResourceScope
    {
        public int Id { get; set; }

        public ProtectedResource ProtectedResource { get; set; }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public bool Emphasize { get; set; }

        public string Name { get; set; }

        public bool Required { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }
    }
}
