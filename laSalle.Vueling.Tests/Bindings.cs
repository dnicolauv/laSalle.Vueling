using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laSalle.Vueling.Tests
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<OpenQA.Selenium.IWebDriver>().To<OpenQA.Selenium.Firefox.FirefoxDriver>();
        }
    }
}
