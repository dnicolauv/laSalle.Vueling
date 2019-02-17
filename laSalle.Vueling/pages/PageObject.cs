using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace laSalle.Vueling.pages
{
    public class PageObject
    {
        public void TypeInto(IWebElement element, String text)
        {
            element.SendKeys(text);
        }

        public ICollection<IWebElement> GetTableRows(IWebElement table)
        {
            var tableCells = table.FindElements(By.TagName("tr"));
            return tableCells;
        }

        public ICollection<IWebElement> GetTableCells(IWebElement table)
        {
            var tableCells = table.FindElements(By.TagName("td"));
            return tableCells;
        }
    }
}