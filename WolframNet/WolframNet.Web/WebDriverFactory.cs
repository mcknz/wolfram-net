
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

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

            ChromeDriverService service =
                ChromeDriverService.CreateDefaultService(
                    settings.GetDriverPath(),
                    GetDriverFile(settings, "chromedriver")
                );

            service.Start();
            return new RemoteWebDriver(service.ServiceUrl, options);
        }

        private String GetDriverFile(Settings settings,
                                     String name)
        {
            if (settings.IsWindows())
            {
                return name + ".exe";
            }
            return name;
        }
    }
}