using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using etoroTask.GoogleTrendsPages;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;
using OpenQA.Selenium.Firefox;
using Protractor;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace etoroTask
{
    [TestFixture]
    [Parallelizable]
    class GoogleTrendsTestChrome
    {
        IWebDriver driver;
        WebDriverWait wait;
        NgWebDriver ngDriver;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
        [OneTimeSetUp]
        public void Initialize()
        {
            log.Info("Initializing Chrome WebDriver");
            driver = new ChromeDriver();
            ngDriver = new NgWebDriver(driver);
            ngDriver.Manage().Window.Maximize();
            wait = new WebDriverWait(ngDriver, TimeSpan.FromSeconds(50));
        }

        [Test]
        public void IsRelatedQueryExistsTest()
        {
            log.Info("Starting 'IsRelatedQueryExistsTest' in Chrome WebDriver");
            try
            {
                driver.Navigate().GoToUrl("https://trends.google.com");

                HomePage trendsHomePage = new HomePage(ngDriver, wait);                
                trendsHomePage.SearchText("Selenium");

                ExplorePage explorePage = new ExplorePage(ngDriver, wait);
                explorePage.SelectCountry("Israel");
                explorePage.SelectSubregionsListView();
                explorePage.SelectSubregion("Tel Aviv District").Should().BeTrue();
                explorePage.CheckRelatedQueries().Should().BeTrue();
            }
            catch (NoSuchElementException e)
            {
                log.Error(e);
                driver.Quit();
            }
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            driver.Quit();
        }

    }

    [TestFixture]
    [Parallelizable]
    class GoogleTrendsTestFireFox
    {
        IWebDriver driver;
        NgWebDriver ngDriver;
        WebDriverWait wait;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [OneTimeSetUp]
        public void Initialize()
        {
            log.Info("Initializing FireFox WebDriver");
            driver = new FirefoxDriver();
            ngDriver = new NgWebDriver(driver);
            ngDriver.Manage().Window.Maximize();
            wait = new WebDriverWait(ngDriver, TimeSpan.FromSeconds(50));
        }

        [Test]
        public void IsRelatedQueryExistsTest()
        {
            log.Info("Starting 'IsRelatedQueryExistsTest' in FireFox WebDriver");
            try
            {
                driver.Navigate().GoToUrl("https://trends.google.com");
                HomePage trendsHomePage = new HomePage(ngDriver, wait);
                trendsHomePage.SearchText("Selenium");

                ExplorePage explorePage = new ExplorePage(ngDriver, wait);
                explorePage.SelectCountry("Israel");
                explorePage.SelectSubregionsListView();
                explorePage.SelectSubregion("Tel Aviv District").Should().BeTrue();
                explorePage.CheckRelatedQueries().Should().BeTrue();
            }
            catch (NoSuchElementException e)
            {
                log.Error(e);
                driver.Quit();
            }          
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            driver.Quit();
        }

    }
}
