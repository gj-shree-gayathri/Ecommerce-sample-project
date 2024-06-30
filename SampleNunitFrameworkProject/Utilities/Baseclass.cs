using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SampleNunitFrameworkProject.Utilities
{
    public class Baseclass
    {
        public ExtentReports extent;
        public ThreadLocal<ExtentTest> test = new ThreadLocal<ExtentTest>();
        /*public IWebDriver driver;*/
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [OneTimeSetUp]
        
        public void ExtendReportMethod()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportpath = $"{projectDirectory}//index.html";
            var htmlreport=new ExtentHtmlReporter(reportpath);
            extent=new ExtentReports();
            extent.AttachReporter(htmlreport);
        }

        [SetUp]
        public void Setup()
        {
            string browsername =ConfigurationManager.AppSettings["browser"];
            BrowserInitiate(browsername);
            test.Value=extent.CreateTest(TestContext.CurrentContext.Test.Name);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");

        }

        public IWebDriver GetDriver()
        {
            return driver.Value;
        }
        public void BrowserInitiate(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
/*                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
*/                    driver.Value = new ChromeDriver();
                    break;
                case "Firefox":
/*                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
*/                    driver.Value = new FirefoxDriver();
                    break;
            }
        }
        public static JsonReader GetJsonReader()
        {
            return new JsonReader();
        }

        [TearDown]
        public void AfterTest()
        {
            DateTime dateTime = DateTime.Now;
            String fileName = $"Screenshot_{dateTime.ToString("h-m-s")}.png";
            var status=TestContext.CurrentContext.Result.Outcome.Status;
            var stacttrace = TestContext.CurrentContext.Result.StackTrace;
            if (status == TestStatus.Failed)
            {
                test.Value.Fail("Test failed", CaptureScreenShot(GetDriver(), fileName));
                test.Value.Log(Status.Fail, stacttrace);
            }
            extent.Flush();
            Thread.Sleep(5000);
            GetDriver().Quit();
        }

        public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver,string screenShotName)
        {
            ITakesScreenshot screenShot=(ITakesScreenshot)driver;
            var scrnShot = screenShot.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(scrnShot, screenShotName).Build();        
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {

            test.Dispose();
            driver.Dispose();
            


        }
    }
}
