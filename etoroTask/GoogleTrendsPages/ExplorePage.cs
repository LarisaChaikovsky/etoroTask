using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;
using System;
using System.Collections.Generic;

namespace etoroTask.GoogleTrendsPages
{
    class ExplorePage : WebPage
    {
        public ExplorePage(NgWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void SelectCountry(string Text)
        {

            log.Info(String.Format("Selecting '{0}' on Explore page", Text));
            NgWebElement geoOptions = driver.FindElement(By.CssSelector("hierarchy-picker[data='ctrl.geoOptions']"));
            wait.Until(driver => geoOptions.Enabled);
            geoOptions.Click();

            NgWebElement geoOptionsInput = geoOptions.FindElement(By.CssSelector("input"));
            wait.Until(driver => geoOptions.Displayed);
            geoOptionsInput.SendKeys(Text);
            geoOptionsInput.SendKeys(Keys.ArrowDown);
            geoOptionsInput.SendKeys(Keys.Enter);         
        }

        public void SelectSubregionsListView()
        {
            log.Info("Selecting Subregions List View");
            if(driver.FindElements(By.CssSelector("widget[type='fe_geo_chart'] div.item")).Count > 0)
            {
                return;
            }
            WaitTillElementIsDisplayed(By.CssSelector("button.toggle-button.list-image"), 200);
            NgWebElement listPicker = driver.FindElement(By.CssSelector("button.toggle-button.list-image"));
            listPicker.Click();
        }

        private IList<NgWebElement> GetSubregionsList()
        {
            log.Info("Getting Subregions List");
            IList<NgWebElement> subregions = driver.FindElements(By.CssSelector("widget[type='fe_geo_chart'] div.item"));

            return subregions;
        }

        private void NavigateToNextPage()
        {
            log.Info("Navigating to the Next Page");
            NgWebElement nextButton = driver.FindElement(By.CssSelector("widget[type='fe_geo_chart'] button[aria-label='Next']"));
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
            IList<NgWebElement> subregions = GetSubregionsList();
            log.Info(String.Format("Checking if '{0}' exists in the Subregions list", Text));

            foreach (NgWebElement subregion in subregions)
            {
                NgWebElement span = subregion.FindElement(By.CssSelector(".label-text span"));
                if (span.Text.Equals(Text))
                {
                    subregion.Click();
                    return true;
                }
            }

            if (HasPagination())
            {
                NavigateToNextPage();
                IList<NgWebElement> additionalSubregions = GetSubregionsList();
                foreach (NgWebElement subregion in additionalSubregions)
                {
                    NgWebElement span = subregion.FindElement(By.CssSelector(".label-text span"));
                    if (span.Text.Equals(Text))
                    {
                        subregion.Click();
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckRelatedQueries()
        {
            log.Info("Cheching if 'Related Queries' appears for choosen district");
            NgWebElement relatedQueriesElement = driver.FindElement(By.Id("RELATED_QUERIES"));

            return true;
        }
    }
}
