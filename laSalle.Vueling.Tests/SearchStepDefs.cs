﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using BoDi;
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

        private static ExtentReports extent;
        private static ExtentTest test;
        private static ExtentReportContext reportContext;
        
        private static SearchPage searchPage;

        private string origin = "";
        private string destination = "";
        private TimePeriod outbound;

        public SearchStepDefs(IObjectContainer container)
        {
            this.objectContainer = container;
            this.objectContainer.RegisterInstanceAs<SeleniumContext>(seleniumContext);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            LOGGER.Debug("BeforeTestRun starts");

            /*REPORT*/
            reportContext = new ExtentReportContext();
            extent = new ExtentReports();
            extent.AttachReporter(reportContext.HtmlReport);
            test = extent.CreateTest("Vueling search flight test", "");
            test.Log(Status.Info, "Test starting");
            /*REPORT*/

            seleniumContext = new SeleniumContext();            
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            LOGGER.Debug("BeforeScenario starts");   
            searchPage = new SearchPage();
            test.Log(Status.Info, "Scenario starting");
        }

        [AfterStep]
        public static void AfterStep()
        {
            string screenShotPath = reportContext.Capture(seleniumContext.WebDriver, ScenarioContext.Current.StepContext.StepInfo.Text);
            test.Log(Status.Info, "Snapshot below: " );
            test.AddScreenCaptureFromPath(screenShotPath);
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            LOGGER.Debug("AfterScenario starts");                     
            
            try
            {
                if(ScenarioContext.Current.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
                {
                    string screenShotBase64 = reportContext.Capture(seleniumContext.WebDriver, "TEST PASSED");
                    test.Log(Status.Pass, "Snapshot below: " );
                    test.AddScreenCaptureFromBase64String(screenShotBase64);
                    //test.Pass("TEST PASSED", 
                    //    MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());
                }
                else
                {
                    string screenShotBase64 = reportContext.Capture(seleniumContext.WebDriver, "TEST FAILED");
                    test.Log(Status.Fail, "Snapshot below: ");
                    test.AddScreenCaptureFromBase64String(screenShotBase64);
                    //test.Fail("TEST PASSED", 
                    //    MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());
                }
            }
            catch(Exception ex)
            {
                string screenShotBase64 = reportContext.Capture(seleniumContext.WebDriver, "TEST ERROR");
                test.Log(Status.Error, "Snapshot below: " );
                test.AddScreenCaptureFromBase64String(screenShotBase64);
                //test.Error("TEST ERROR", 
                //        MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());
            }
            finally
            {                
                seleniumContext.WebDriver.Quit();
                extent.Flush();
            }            
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
            SearchOrigin(origin);
            SearchDestination(destination);
            SelectDate(outbound);            
            Search();            
        }        

        [Then(@"I get available flight")]
        public void ThenIGetAvailableFlight()
        {
            Assert.IsTrue(seleniumContext.WebDriver.Url == searchPage.SCHEDULE_URL);           
        }   
        
        public void GetSpecFlowValues(Table table)
        {
            SpecFlowTableReader specFlowReader = new SpecFlowTableReader(table);
            origin = specFlowReader.GetValue("Origin");
            destination = specFlowReader.GetValue("Destination");
            outbound = TimePeriod.GetInstance(specFlowReader.GetValue("Outbound"));
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
