using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace laSalle.Vueling.Tests
{
    [Binding]
    public class SearchStepDefs
    {
        private IWebDriver _driver;

        public SearchStepDefs(IWebDriver driver)
        {
            _driver = driver;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Close();
        }

        [Given(@"I'm main page")]
        public void GivenIMMainPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I try to find a fly")]
        public void WhenITryToFindAFly(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I get available flight")]
        public void ThenIGetAvailableFlight()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
