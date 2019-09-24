
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Runtime.InteropServices;

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
            switch (settings.DriverType)
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
                options.AddArguments("headless", "window-size=1920,1080");
            }

            /*
            ChromeDriverService service =
#pragma warning disable IDE0067 // Dispose objects before losing scope
                                // reason: disposing of service causes connection to Chrome to fail
                ChromeDriverService.CreateDefaultService(
                    settings.DriverPath, GetDriverExeName("chromedriver")
                );
#pragma warning restore IDE0067 // Dispose objects before losing scope

            service.Start();
            return new RemoteWebDriver(service.ServiceUrl, options);
            */
            return new ChromeDriver(settings.DriverPath, options);
        }

        private string GetDriverExeName(string baseName)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? baseName + ".exe"
                : baseName;
        }
    }
}