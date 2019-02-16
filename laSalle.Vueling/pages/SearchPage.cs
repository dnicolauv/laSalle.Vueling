using laSalle.Vueling.domain;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace laSalle.Vueling.pages
{
    public class SearchPage 
    {
        private static ILog LOGGER = LogManager.GetLogger(typeof(SearchPage));

        public IWebElement SelectFrom {get;set;}
        public IWebElement SelectTo {get;set;}
        public IWebElement SelectDates {get;set;}
        public IWebElement BtnSearch {get;set;}

        public void Search(SearchDto search) {
            LOGGER.Debug($"Search starts, search: {search}");

            SelectFrom.Click();


        }
    }
}