using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace WolframNet.Web
{
    public static class IWebDriverExtensions
    {

        // this method creates a new reference to IWebElement for each operation to avoid StaleElementReferenceException
        // see https://docs.seleniumhq.org/exceptions/stale_element_reference.jsp
        public static void ClearAndEnterIfDifferent(this IWebDriver driver, string elementId, string text)
        {
            if (driver.WaitUntilElementClickable(elementId).Text != text)
            {
                driver.WaitUntilElementClickable(elementId).Clear();
                driver.WaitUntilElementClickable(elementId).SendKeys(text);
            }
        }

        public static IWebElement WaitUntilElementClickable(this IWebDriver driver, string elementId, int timeout = 20)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                return wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(elementId)));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementId + "' was not found in current context page.");
                throw;
            }
        }
    }
}
