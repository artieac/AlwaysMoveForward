using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.Configuration
{
    public class LoggingConfiguration
    {

        public LoggingConfiguration() { }

        public string Source { get; set; }

        public string Level { get; set; }

        public string LoggingClass { get; set; }

        public string LoggingAssembly { get; set; }
    }
}