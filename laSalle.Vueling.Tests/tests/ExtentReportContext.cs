using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace laSalle.Vueling.Tests.tests
{
    class ExtentReportContext
    {
        public SeleniumContext SeleniumContext { get; internal set; }

        public ExtentHtmlReporter HtmlReport {get;set;} = new ExtentHtmlReporter("reports\\ExtentScreenshot.html");
        public string ProjectPath {get;set;}
        public string ReportPath {get;set;}
        public string ImagePath {get;set;}

        public ExtentReports Extent {get;set; }
        public ExtentTest Test {get;set; }

        public ExtentReportContext(SeleniumContext seleniumContext)
        {
            SeleniumContext = seleniumContext;
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

            Extent = new ExtentReports();
            Extent.AttachReporter(HtmlReport);
            Test = Extent.CreateTest("Vueling search flight test", "");
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

        public void TakeScreenshotAndLog(Status status, string text)
        {
            string screenShotPath = Capture(SeleniumContext.WebDriver, text);
            this.Test.Log(status, text);
            this.Test.AddScreenCaptureFromPath(screenShotPath);
        }

        public void Log(Status status, string text)
        {
            this.Test.Log(status, text);
        }

        public void Flush()
        {
            this.Extent.Flush();
        }
    }
}
