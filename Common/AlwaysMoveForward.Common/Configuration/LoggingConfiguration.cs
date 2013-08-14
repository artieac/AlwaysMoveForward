using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.Common.Configuration
{
    public class LoggingConfiguration : ConfigurationSection
    {
        public const string k_DefaultConfigurationSetting = "AlwaysMoveForward/LoggingConfiguration";

        private const string k_Source = "Source";
        private const string k_Level = "Level";
        private const string k_LoggingClass = "LoggingClass";
        private const string k_LoggingAssembly = "LoggingAssembly";

        public LoggingConfiguration() { }

        [ConfigurationProperty(k_Source, IsRequired = true)]
        public String Source
        {
            get { return (string)this[k_Source]; }
            set { this[k_Source] = value; }
        }

        [ConfigurationProperty(k_Level, IsRequired = true)]
        public String Level
        {
            get { return (string)this[k_Level]; }
            set { this[k_Level] = value; }
        }

        [ConfigurationProperty(k_LoggingClass, IsRequired = false)]
        public String LoggingClass
        {
            get { return (string)this[k_LoggingClass]; }
            set { this[k_LoggingClass] = value; }
        }

        [ConfigurationProperty(k_LoggingAssembly, IsRequired = false)]
        public String LoggingAssembly
        {
            get { return (string)this[k_LoggingAssembly]; }
            set { this[k_LoggingAssembly] = value; }
        }
    }
}