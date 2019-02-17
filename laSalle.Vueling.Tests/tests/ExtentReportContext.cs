using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace laSalle.Vueling.Tests.tests
{
    class ExtentReportContext
    {
        public ExtentHtmlReporter HtmlReport {get;set;} = new ExtentHtmlReporter("reports\\ExtentScreenshot.html");
        public string ProjectPath {get;set;}
        public string ReportPath {get;set;}
        public string ImagePath {get;set;}

        public ExtentReportContext()
        {
            Startup();
        }

        public void Startup()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            ProjectPath = new Uri(actualPath).LocalPath;
            ReportPath = ProjectPath + "reports\\ExtentScreenshot.html";
            ImagePath = ProjectPath + "reports";
            HtmlReport = new ExtentHtmlReporter(ReportPath);
        }      
        
        public string Capture(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string finalpth = ImagePath + "\\" + screenShotName + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return localpath;
        }
    }
}
