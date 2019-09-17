using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WolframNet.Web
{
    public class Settings
    {
        private readonly IConfiguration config;
        private WebDriverType driverType;
        private string driverPath;
        private bool? isWindows;
        private TimeSpan pageTimeout;
        
        public Settings() {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public WebDriverType getDriverType()
        {
            if(driverType == WebDriverType.None)
            {
                if (!Enum.TryParse(GetVariable("driverType"), out driverType))
                {
                    throw new Exception("Incorrect driver type.");
                }
            }
            return driverType;
            
        }

        public string GetDriverPath()
        {
            if(driverPath == null)
            {
                driverPath = GetVariable("driverPath");
            }
            return driverPath;
        }

        public bool IsWindows()
        {
            if (!isWindows.HasValue)
            {
                string os = GetVariable("operatingSystem");
                if(os.ToLower().Equals("windows"))
                {
                    isWindows = true;
                }
            }
            return isWindows.Value;
        }

        public TimeSpan GetPageTimeout()
        {
            if (pageTimeout == null)
            {
                if(!int.TryParse(GetVariable("pageTimeout"), out int pageTimeoutSeconds))
                {
                    throw new Exception("Invalid page timeout.");
                }
                else
                {
                    pageTimeout = TimeSpan.FromSeconds(pageTimeoutSeconds);
                }
            }
            return pageTimeout;
        }

        private string GetVariable(String key)
        {
            String variable = Environment.GetEnvironmentVariable(key);
            if (variable == null)
            {
                return config[key];
            }
            return variable;
        }
    }
}
