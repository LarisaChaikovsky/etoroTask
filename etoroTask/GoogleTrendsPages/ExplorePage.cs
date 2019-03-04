using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium.Support.Extensions;
using System.Linq;

namespace etoroTask.GoogleTrendsPages
{
    class ExplorePage : WebPage
    {
        public ExplorePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void SelectCountry(string Text)
        {           
            IWebElement geoOptions = driver.FindElement(By.CssSelector("hierarchy-picker[data='ctrl.geoOptions']"));
            wait.Until(driver => geoOptions.Enabled);
            geoOptions.Click();

            IWebElement geoOptionsInput = geoOptions.FindElement(By.CssSelector("input"));
            wait.Until(driver => geoOptions.Displayed);
            geoOptionsInput.SendKeys(Text);
            geoOptionsInput.SendKeys(Keys.ArrowDown);
            geoOptionsInput.SendKeys(Keys.Enter);         
        }

        public void SelectSubregionsListView()
        {            
            WaitTillElementIsDisplayed(By.CssSelector("button.toggle-button.list-image"), 200);
            //WaitForElementLoad(By.CssSelector("button.toggle-button.list-image"), 1000);
            IWebElement listPicker = driver.FindElement(By.CssSelector("button.toggle-button.list-image"));
            listPicker.Click();
        }

        private IList<IWebElement> GetSubregionsList()
        {
            IList<IWebElement> subregions = driver.FindElements(By.CssSelector("widget[type='fe_geo_chart'] div.item"));

            return subregions;
        }

        private void NavigateToNextPage()
        {
            IWebElement nextButton = driver.FindElement(By.CssSelector("widget[type='fe_geo_chart'] button[aria-label='Next']"));
            nextButton.Click();
        }
        private bool HasPagination()
        {
            if(driver.FindElement(By.CssSelector("widget[type='fe_geo_chart'] button[aria-label='Next']")).Enabled)
                return true;
            return false;
        }

        public bool SelectSubregion(string Text)
        {
            IList<IWebElement> subregions = GetSubregionsList();

            foreach (IWebElement subregion in subregions)
            {
                IWebElement span = subregion.FindElement(By.CssSelector(".label-text span"));
                if (span.Text.Equals(Text))
                {
                    subregion.Click();
                    return true;
                }
            }

            if (HasPagination())
            {
                NavigateToNextPage();
                IList<IWebElement> additionalSubregions = GetSubregionsList();
                foreach (IWebElement subregion in additionalSubregions)
                {
                    IWebElement span = subregion.FindElement(By.CssSelector(".label-text span"));
                    if (span.Text.Equals(Text))
                    {
                        subregion.Click();
                        return true;
                    }
                }
            }

            return false;
        }

        public Boolean CheckRelatedQueries()
        {
            IWebElement relatedQueriesElement = driver.FindElement(By.Id("RELATED_QUERIES"));

            return true;
        }
    }
}
