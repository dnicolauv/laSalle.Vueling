using BoDi;
using laSalle.Vueling.pages;
using laSalle.Vueling.Tests.services;
using laSalle.Vueling.Tests.tests;
using log4net;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace laSalle.Vueling.Tests
{
    [Binding]
    public class SearchStepDefs
    {
        private static ILog LOGGER = LogManager.GetLogger(typeof(SearchStepDefs));
        private IObjectContainer objectContainer;
        private static SeleniumContext seleniumContext ;
        //private SearchService searchService;
        //private SearchPage reservationPage;

        public SearchStepDefs(IObjectContainer container)
        {
            this.objectContainer = container;
            objectContainer.RegisterInstanceAs<SeleniumContext>(seleniumContext );
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            LOGGER.Debug("BeforeTestRun starts");
            seleniumContext = new SeleniumContext();
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            LOGGER.Debug("BeforeScenario starts");
            
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            seleniumContext.WebDriver.Close();
        }

        [Given(@"I'm main page")]
        public void GivenIMMainPage()
        {
            //_driver = new FirefoxDriver();
            seleniumContext.WebDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            seleniumContext.WebDriver.Manage().Window.Maximize();
            seleniumContext.WebDriver.Url = "https://www.vueling.com/es";
        }
        
        [When(@"I try to find a fly")]
        public void WhenITryToFindAFly(Table table)
        {
            //reservationPage.TypeInto(reservationPage.SelectFromTextBox, table.)
            string a = "B";
        }
        
        [Then(@"I get available flight")]
        public void ThenIGetAvailableFlight()
        {
            //ScenarioContext.Current.Pending();
        }
    }
}
