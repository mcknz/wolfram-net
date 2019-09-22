using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WolframNet.Web
{
    public class Settings
    {
        private readonly IConfiguration config;
        private WebDriverType driverType;
        private TimeSpan? pageTimeout;
        private string testRoot;
        
        public Settings() {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();
        }

        public WebDriverType DriverType
        {
            get
            {
                if (driverType == WebDriverType.None)
                {
                    if (!Enum.TryParse(GetVariable("driverType"), out driverType))
                    {
                        throw new Exception("Incorrect driver type.");
                    }
                }
                return driverType;
            }
            
        }

        public TimeSpan PageTimeout
        {
            get
            {
                if (pageTimeout == null)
                {
                    if (!int.TryParse(GetVariable("pageTimeout"), out int pageTimeoutSeconds))
                    {
                        throw new Exception("Invalid page timeout.");
                    }
                    else
                    {
                        pageTimeout = TimeSpan.FromSeconds(pageTimeoutSeconds);
                    }
                }
                return pageTimeout.Value;
            }
        }

        public string DriverPath
        {
            get
            {
                testRoot = GetVariable("driverPath");
                return testRoot;
            }
        }

        private string GetVariable(String key)
        {
            string variable = Environment.GetEnvironmentVariable(key);
            if (variable == null)
            {
                return config[key];
            }
            return variable;
        }
    }
}
