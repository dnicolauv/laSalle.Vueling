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
    }
}