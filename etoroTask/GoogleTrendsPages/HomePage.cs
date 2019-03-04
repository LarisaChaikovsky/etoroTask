using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace etoroTask.GoogleTrendsPages
{
    class HomePage : WebPage
    {
        public HomePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }
       
        public void SearchText(string Text)
        {
            IWebElement searchInput = driver.FindElement(By.CssSelector(".home-page-header-container .home-page-header-autocomplete-container input[type='search']"));
            wait.Until(driver => searchInput);
            searchInput.Click();
            searchInput.SendKeys(Text);
            searchInput.SendKeys(Keys.Enter);
        }
    }
}
