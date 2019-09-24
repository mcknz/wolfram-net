using OpenQA.Selenium;

namespace WolframNet.Web {

    public class Driver
    {
        private static readonly WebDriverFactory factory = new WebDriverFactory();
        private static IWebDriver driver;
        private static readonly Settings settings;

        static Driver()
        {
            settings = new Settings();
        }

        public static IWebDriver Get()
        {
            if(driver == null)
            {
                driver = factory.GetDriver(settings);
            }
            return driver;
        }

        public static Settings GetSettings()
        {
            return settings;
        }

        public static void Quit()
        {
            //if (driver != null)
            //{
            //throw new System.Exception("quitting......");
                driver.Close();
                driver.Quit();
                driver.Dispose();
                driver = null;
            //}
        }
    }
}