using laSalle.Vueling.domain;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace laSalle.Vueling.pages
{
    public class SearchPage : PageObject
    {
        private static ILog LOGGER = LogManager.GetLogger(typeof(SearchPage));

        public IWebElement SelectFrom {get;set;}
        public IWebElement SelectFromTextBox {get;set;}
        public IWebElement SelectTo {get;set;}
        public IWebElement SelectToTextBox {get;set;}
        public IWebElement SelectDates {get;set;}
        public IWebElement BtnSearch {get;set;}

        public void Search(SearchDto search) {
            LOGGER.Debug($"Search starts, search: {search}");

            SelectFrom.Click();
            
            TypeInto(SelectFromTextBox, search.FromLocation);
            TypeInto(SelectFromTextBox, Keys.Enter);

            TypeInto(SelectFromTextBox, search.ToLocation);
            TypeInto(SelectFromTextBox, Keys.Enter);

            SelectDates.Click();
            
            BtnSearch.Click();
        }
    }
}