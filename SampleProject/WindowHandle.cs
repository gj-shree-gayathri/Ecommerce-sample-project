using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject
{
    public class WindowHandle
    {
        ChromeDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
        }
        [Test]
        public void WindowHandleCheck()
        {
            string currentWindow = driver.CurrentWindowHandle;
            driver.FindElement(By.CssSelector(".blinkingText")).Click();
            var tabs=driver.WindowHandles.ToList();
            string childwindow=driver.WindowHandles[1];
            driver.SwitchTo().Window(childwindow);
            string email=driver.FindElement(By.XPath("//strong//a")).Text;
            driver.SwitchTo().Window(currentWindow);
            driver.FindElement(By.Id("username")).SendKeys(email);
            Thread.Sleep(5000);

        }
        [OneTimeTearDown]
        public void TestTearDown()
        {
            /*driver.Quit();*/
            driver.Dispose();
        }
    }
}
