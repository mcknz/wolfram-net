
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

namespace WolframNet.Web
{
    class WebDriverFactory
    {
        internal IWebDriver GetDriver(Settings settings)
        {
            return GetWebDriver(settings);
        }

        private IWebDriver GetWebDriver(Settings settings)
        {
            switch (settings.getDriverType())
            {
                case WebDriverType.Chrome:
                    return GetChromeDriver(settings, false);
                case WebDriverType.HeadlessChrome:
                    return GetChromeDriver(settings, true);
            }
            throw new ArgumentException("Unable to create Driver for the specified type.");
        }

        private IWebDriver GetChromeDriver(Settings settings,
                                           bool isHeadless)
        {
            ChromeOptions options = new ChromeOptions();
            if (isHeadless)
            {
                options.AddArguments("headless");
            } 

            using (ChromeDriverService service =
                ChromeDriverService.CreateDefaultService(
                    GetAppRoot()
                )
            )
            {
                service.Start();
                return new RemoteWebDriver(service.ServiceUrl, options);
            }
        }

        private string GetAppRoot()
        {
            string codeBaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string appRoot = codeBaseDirectory.Substring(0, codeBaseDirectory.LastIndexOf("\\bin")) + "\\";
            return appRoot;
        }
    }
}