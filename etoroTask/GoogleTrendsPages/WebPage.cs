using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;
using System;
using System.Threading;

namespace etoroTask.GoogleTrendsPages
{
    class WebPage
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected readonly NgWebDriver driver;
        protected readonly WebDriverWait wait;

        protected WebPage(NgWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public bool WaitTillElementIsDisplayed(By selector, int timeoutInSeconds)
        {
            bool elementDisplayed = false;

            for (int i = 0; i < timeoutInSeconds; i++)
            {
                try
                {
                    if (timeoutInSeconds > 0)
                    {
                        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                        wait.Until(driver => driver.FindElement(selector));
                    }
                    elementDisplayed = driver.FindElement(selector).Displayed;
                }
                catch(Exception e)
                {
                    Thread.Sleep(timeoutInSeconds);
                    return false;
                }
            }
            return elementDisplayed;

        }

        public void WaitForElementLoad(By selector, int waitMilliseconds)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(d =>
            {
                try
                {
                    driver.FindElement(selector);
                    return true;
                }
                catch (Exception)
                {
                    Thread.Sleep(waitMilliseconds);
                    return false;
                }
            });
        }
    }
}
