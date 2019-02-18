using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using BoDi;
using laSalle.Vueling.domain;
using laSalle.Vueling.pages;
using laSalle.Vueling.Tests.builders;
using laSalle.Vueling.Tests.domain;
using laSalle.Vueling.Tests.services;
using laSalle.Vueling.Tests.tests;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace laSalle.Vueling.Tests
{
    [Binding]
    public class SearchStepDefs
    {
        private static ILog LOGGER = LogManager.GetLogger(typeof(SearchStepDefs));
        private IObjectContainer objectContainer;
        
        private static SeleniumContext seleniumContext;
        private static ExtentReportContext reportContext;
        
        private static SearchPage searchPage;
        private Search search;

        public SearchStepDefs(IObjectContainer container)
        {
            this.objectContainer = container;
            this.objectContainer.RegisterInstanceAs<SeleniumContext>(seleniumContext);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            LOGGER.Debug("BeforeTestRun starts");
            seleniumContext = new SeleniumContext();            
            reportContext = new ExtentReportContext(seleniumContext);
            reportContext.Log(Status.Info, "Test starting");          
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            LOGGER.Debug("BeforeScenario starts");   
            searchPage = new SearchPage();
            reportContext.Log(Status.Info, "Scenario starting");
        }

        [AfterStep]
        public static void AfterStep()
        {
            LOGGER.Debug("AftrStep starts");   
            reportContext.TakeScreenshotAndLog(Status.Info, ScenarioContext.Current.StepContext.StepInfo.Text);        
        }
      
        [Given(@"I'm main page")]
        public void GivenIMMainPage()
        {
            seleniumContext.WebDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            seleniumContext.WebDriver.Manage().Window.Maximize();
            seleniumContext.WebDriver.Url = searchPage.HOME_URL;            
        }
        
        [When(@"I try to find a fly")]
        public void WhenITryToFindAFly(Table table)
        {    
            GetSpecFlowValues(table);            
            StartSearching();
            SearchOrigin(search.FromLocation);
            SearchDestination(search.ToLocation);
            SelectDate(search.OnDate);            
            Search();            
        }        

        [Then(@"I get available flight")]
        public void ThenIGetAvailableFlight()
        {
            Assert.IsTrue(seleniumContext.WebDriver.Url == searchPage.SCHEDULE_URL);           
        }   
        
        [AfterScenario]
        public static void AfterScenario()
        {
            LOGGER.Debug("AfterScenario starts");                     
            
            try
            {
                if(ScenarioContext.Current.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
                    reportContext.TakeScreenshotAndLog(Status.Pass, "TEST PASSED");   
                else
                    reportContext.TakeScreenshotAndLog(Status.Fail, "TEST FAILED");                  
            }
            catch(Exception ex)
            {
                reportContext.TakeScreenshotAndLog(Status.Error, "TEST ERROR");   
            }
            finally
            {                
                seleniumContext.WebDriver.Quit();
                reportContext.Flush();
            }            
        }

        public void GetSpecFlowValues(Table table)
        {
            SpecFlowTableReader specFlowReader = new SpecFlowTableReader(table);
            search = new Search();
            search.FromLocation = specFlowReader.GetValue("Origin");
            search.ToLocation = specFlowReader.GetValue("Destination");
            search.OnDate = TimePeriod.GetInstance(specFlowReader.GetValue("Outbound"));
        }

        public void StartSearching()
        {
            searchPage.WebElementSelectFrom = seleniumContext.WebDriver.FindElement(By.CssSelector("#tab-search > div > form > div:nth-child(1) > div.form-input.origin"));
            searchPage.StartSearch();
        }

        public void SearchOrigin(string origin)
        {
            searchPage.WebElementSelectFromTextBox = seleniumContext.WebDriver.FindElement(By.XPath("//*[@id='tab-search']/div/form/div[1]/div[1]/div[1]/input"));
            searchPage.SearchFrom(origin);
        }

        public void SearchDestination(string destination)
        {
            searchPage.WebElementSelectToTextBox = seleniumContext.WebDriver.FindElement(By.XPath("//*[@id='tab-search']/div/form/div[1]/div[2]/div[1]/input"));
            searchPage.SearchTo(destination);
        }

        private void SelectDate(TimePeriod outbound)
        {
            searchPage.WebElementSelectDates = seleniumContext.WebDriver.FindElements(By.ClassName("ui-datepicker-calendar"));            
            searchPage.SelectDate(outbound);   
        }

        public void Search()
        {                        
            searchPage.WebElementBtnSearch = seleniumContext.WebDriver.FindElement(By.Id(("btnSubmitHomeSearcher")));
            searchPage.Search();
        }
    }
}
