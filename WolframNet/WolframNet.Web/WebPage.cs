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

        public WebPage(string url)
        {
            driver = Driver.Get();
            settings = Driver.GetSettings();
            this.url = url;
        }

        public void NavigateToStartingUrl()
        {
            driver.Navigate().GoToUrl(url);
        }

        protected void MouseoverByXPath(String xPath)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(WaitUntilElementClickableByXPath(xPath)).Perform();
        }

        protected void EnterTextByXPath(String xPath, String text)
        {
            WaitUntilElementClickableByXPath(xPath).SendKeys(text);
        }

        protected String GetTextByXPath(String xPath)
        {
            return WaitUntilElementClickableByXPath(xPath).Text;
        }

        protected void ClickByXPath(String xPath)
        {
            WaitUntilElementClickableByXPath(xPath).Click();
        }

        private IWebElement WaitUntilElementClickableByXPath(String xPath)
        {
            return WaitUntilElementClickable(By.XPath(xPath));
        }

        private IWebElement WaitUntilElementClickable(By locator)
        {
            return GetWait().Until(ExpectedConditions.ElementToBeClickable(locator));
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
    }
}