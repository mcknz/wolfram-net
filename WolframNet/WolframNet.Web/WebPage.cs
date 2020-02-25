using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace WolframNet.Web
{
    public abstract class WebPage
    {
        private readonly IWebDriver driver;
        private readonly Settings settings;
        private readonly string url;
        private readonly ILogger<WebPage> logger;

        public WebPage(string url, ILogger<WebPage> logger)
        {
            driver = Driver.Get();
            settings = Driver.GetSettings();
            this.url = url;
            this.logger = logger;
        }

        public void NavigateToStartingUrl()
        {
            LogInfo("Navigating to {url}", url);
            driver.Navigate().GoToUrl(url);
        }

        protected void MouseoverByXPath(String xPath)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(WaitUntilElementClickableByXPath(xPath)).Perform();
        }

        protected void EnterTextByXPath(string xPath, string text)
        {
            WaitUntilElementClickableByXPath(xPath).SendKeys(text);
        }

        protected String GetTextByXPath(string xPath)
        {
            return WaitUntilElementClickableByXPath(xPath).Text;
        }

        protected void ClickByXPath(string xPath)
        {
            WaitUntilElementClickableByXPath(xPath).Click();
        }

        private IWebElement WaitUntilElementClickableByXPath(string xPath)
        {
            LogInfo("Waiting until {xPath} is clickable", xPath);
            return WaitUntilElementClickable(By.XPath(xPath));
        }

        private IWebElement WaitUntilElementClickable(By locator)
        {
            WebDriverWait wait = GetWait();
            IWebElement element = null;
            try
            {
                element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch(Exception ex) when (LogError(ex, "error retrieving web element"))
            {
            }
            return element;
        }

        private WebDriverWait GetWait()
        {
            WebDriverWait wait = new WebDriverWait(driver, settings.PageTimeout);
            wait.IgnoreExceptionTypes(
                typeof(StaleElementReferenceException),
                typeof(NoSuchElementException)
            );
            return wait;
        }

        private void LogInfo(string message, string param)
        {
            string logMessage = $"[info] {message}";
            logger.LogInformation(logMessage, param);
        }

        private bool LogError(Exception ex, string message)
        {
            string logMessage = $"[error] {message}";
            logger.LogError(ex, logMessage);
            return false;
        }
    }
}