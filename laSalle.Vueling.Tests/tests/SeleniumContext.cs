using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laSalle.Vueling.Tests.tests
{
    public class SeleniumContext
    {
         public SeleniumContext()
         {
              WebDriver = new OpenQA.Selenium.Firefox.FirefoxDriver();
         }

        public IWebDriver WebDriver{get; private set;} 
    }
}
