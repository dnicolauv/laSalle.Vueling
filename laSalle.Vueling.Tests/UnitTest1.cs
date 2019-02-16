using System;
using log4net;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace laSalle.Vueling.Tests
{
    [TestFixture]
    public class UnitTest1
    {        
        private static ILog LOGGER = LogManager.GetLogger(typeof(UnitTest1));

        private static IWebDriver driver;
            
        [SetUp]
        public void BeforeClass() 
        {                
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            driver.Manage().Window.Maximize();
            driver.Url =  "https://www.vueling.com/es";              
            LOGGER.Debug("driver started");
        }

        [TearDown]
        public void AfterClass()
        {
            driver.Close();
            LOGGER.Debug("driver closed");
        }

        [Test]
        public void Test() 
        {
            LOGGER.Debug("start Test");

            //Origin
            IWebElement origin = driver.FindElement(By.CssSelector("#tab-search > div > form > div:nth-child(1) > div.form-input.origin"));
            origin.Click();
            
            IWebElement originTxt = driver.FindElement(By.XPath("//*[@id='tab-search']/div/form/div[1]/div[1]/div[1]/input"));
            originTxt.SendKeys("Alicante");
            originTxt.SendKeys(Keys.Enter);

            //IWebElement alicanteOption = driver.FindElement(By.CssSelector("#origin-sugestion-popup > div:nth-child(2) > ul > li:nth-child(5)"));
            //alicanteOption.Click();

            IWebElement destinationTxt = driver.FindElement(By.XPath("//*[@id='tab-search']/div/form/div[1]/div[2]/div[1]/input"));            
            destinationTxt.SendKeys("Aalborg");
            destinationTxt.SendKeys(Keys.Enter);
            
            IWebElement startDate = driver.FindElement(By.CssSelector("#ui-datepicker-div > div.ui-datepicker-group.ui-datepicker-group-first > table > tbody > tr:nth-child(4) > td:nth-child(6) > a"));
            startDate.Click();

            IWebElement searchButton = driver.FindElement(By.Id(("btnSubmitHomeSearcher")));
            searchButton.Click();
        }        
    }
}
