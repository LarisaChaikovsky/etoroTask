using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using etoroTask.GoogleTrendsPages;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;
using log4net;
using OpenQA.Selenium.Firefox;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace etoroTask
{
    [TestFixture]
    [Parallelizable]
    class GoogleTrendsTestChrome
    {
        IWebDriver driver;
        WebDriverWait wait;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
        [OneTimeSetUp]
        public void Initialize()
        {
            log.Info("Initializing Chrome WebDriver");
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
        }

        [Test]
        public void IsRelatedQueryExistsTest()
        {
            log.Info("Starting 'IsRelatedQueryExistsTest' in Chrome WebDriver");
            try
            {
                driver.Navigate().GoToUrl("https://trends.google.com");
                driver.Manage().Window.Maximize();
                HomePage trendsHomePage = new HomePage(driver, wait);
                log.Info("Searching 'Selenium' on 'https://trends.google.com' Home page");
                trendsHomePage.SearchText("Selenium");
                ExplorePage explorePage = new ExplorePage(driver, wait);
                log.Info("Selecting 'Israel' on Explore page");
                explorePage.SelectCountry("Israel");
                log.Info("Selecting Subregions List View");
                explorePage.SelectSubregionsListView();
                log.Info("Cheching if 'Tel Aviv District' exists in the Subregions list");
                explorePage.SelectSubregion("Tel Aviv District").Should().BeTrue();
                log.Info("Cheching if 'Related Queries' appears for choosen district");
                explorePage.CheckRelatedQueries().Should().BeTrue();
            }
            catch (NoSuchElementException e)
            {
                log.Error(e);
                driver.Close();
            }

        }

        [OneTimeTearDown]
        public void EndTest()
        {
            driver.Close();
        }

    }

    [TestFixture]
    [Parallelizable]
    class GoogleTrendsTestFireFox
    {
        IWebDriver driver;
        WebDriverWait wait;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [OneTimeSetUp]
        public void Initialize()
        {
            log.Info("Initializing FireFox WebDriver");
            driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
        }

        [Test]
        public void IsRelatedQueryExistsTest()
        {
            log.Info("Starting 'IsRelatedQueryExistsTest' in FireFox WebDriver");
            try
            {
                driver.Navigate().GoToUrl("https://trends.google.com");
                driver.Manage().Window.Maximize();
                HomePage trendsHomePage = new HomePage(driver, wait);
                log.Info("Searching 'Selenium' on 'https://trends.google.com' Home page");
                trendsHomePage.SearchText("Selenium");
                ExplorePage explorePage = new ExplorePage(driver, wait);
                log.Info("Selecting 'Israel' on Explore page");
                explorePage.SelectCountry("Israel");
                log.Info("Selecting Subregions List View");
                explorePage.SelectSubregionsListView();
                log.Info("Cheching if 'Tel Aviv District' exists in the Subregions list");
                explorePage.SelectSubregion("Tel Aviv District").Should().BeTrue();
                log.Info("Cheching if 'Related Queries' appears for choosen district");
                explorePage.CheckRelatedQueries().Should().BeTrue();
            }
            catch (NoSuchElementException e)
            {
                log.Error(e);
                driver.Close();
            }          
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            driver.Close();
        }

    }
}
