using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.Configuration
{
    public class WhiteListConfiguration
    { 
        /// <summary>
        /// Gets or sets the Request Token URI string found in the config file.
        /// </summary>
        public string FolderWhitelist { get; set; }

        /// <summary>
        /// Gets or sets the Request Token URI string found in the config file.
        /// </summary>
        public string FileTypeWhitelist { get; set; }
    }
}
