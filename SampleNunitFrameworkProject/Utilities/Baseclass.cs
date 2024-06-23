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
        public IWebDriver driver;
        

        [SetUp]
        public void Setup()
        {
            string browsername=ConfigurationManager.AppSettings["browser"];
            BrowserInitiate(browsername);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");

        }

        public IWebDriver GetDriver()
        {
            return driver;
        }
        public void BrowserInitiate(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
/*                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
*/                    driver = new ChromeDriver();
                    break;
                case "Firefox":
/*                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
*/                    driver = new FirefoxDriver();
                    break;
            }
        }
        [TearDown]
        public void TestTearDown()
        {
            /*driver.Quit();*/
            Thread.Sleep(10000);
            driver.Dispose();
        }
    }
}
