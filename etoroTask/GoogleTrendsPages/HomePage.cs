using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Protractor;

namespace etoroTask.GoogleTrendsPages
{
    class HomePage : WebPage
    {
        public HomePage(NgWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        //[FindsBy(How = How.CssSelector, Using = ".home-page-header-container .home-page-header-autocomplete-container input[type='search']")]
       // private IWebElement searchInput;

        public void SearchText(string Text)
        {
            log.Info(string.Format("Searching '{0}' on '{1}' Home page", Text, driver.Url));
            NgWebElement searchInput = driver.FindElement(By.CssSelector(".home-page-header-container .home-page-header-autocomplete-container input[type='search']"));
            wait.Until(driver => searchInput);
            searchInput.Click();
            searchInput.SendKeys(Text);
            searchInput.SendKeys(Keys.Enter);
        }
    }
}
