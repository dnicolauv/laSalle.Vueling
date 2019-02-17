using laSalle.Vueling.domain;
using laSalle.Vueling.Tests.domain;
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

        public string HOME_URL {get;set;} = "https://www.vueling.com/es";
        public string SCHEDULE_URL { get; set; } = "https://tickets.vueling.com/ScheduleSelect.aspx";
        public IWebElement WebElementSelectFrom {get;set;}
        public IWebElement WebElementSelectFromTextBox {get;set;}
        public IWebElement WebElementSelectTo {get;set;}
        public IWebElement WebElementSelectToTextBox {get;set;}
        public IReadOnlyCollection<IWebElement> WebElementSelectDates {get;set;}
        public IWebElement WebElementBtnSearch {get;set;}       

        public void Search(SearchDto search) {
            LOGGER.Debug($"Search starts, search: {search}");

            StartSearch();

            SearchFrom(search.FromLocation);
            
            SearchTo(search.ToLocation);

            SelectDate(search.OnDate);

            Search(); 
        }

        public void StartSearch()
        {                        
            WebElementSelectFrom.Click();
        }

        public void SearchFrom(String location)
        {
            TypeInto(WebElementSelectFromTextBox, location);
            TypeInto(WebElementSelectFromTextBox, Keys.Enter);
        }

        public void SearchTo(String location)
        {
            TypeInto(WebElementSelectToTextBox, location);
            TypeInto(WebElementSelectToTextBox, Keys.Enter);
        }

        public void SelectDate(TimePeriod timePeriod)
        {
            foreach(var table in WebElementSelectDates)
            {
                var cells = GetTableCells(table);

                var date = SearchAvailableDate(cells, timePeriod);

                if(date != null)
                {
                    date.Click();
                    break;
                }
            }
        }        

        public IWebElement SearchAvailableDate(ICollection<IWebElement> cells, TimePeriod timePeriod)
        {
            var period = timePeriod.GetDateTime();
            
            var dateCell = cells.Where(x => x.GetAttribute("data-month") == (period.Month - 1).ToString() &&
                                                  x.GetAttribute("data-year") == period.Year.ToString() &&
                                                  x.Text == period.Day.ToString())
                                                  .FirstOrDefault();

            return dateCell;
        }

        public void Search()
        {
             WebElementBtnSearch.Click();
        }       
    }
}