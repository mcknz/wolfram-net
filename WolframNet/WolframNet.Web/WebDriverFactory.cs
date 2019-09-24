
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            
            return new ChromeDriver(settings.DriverPath, options);
        }
    }
}