using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject
{
    public class AlertExample
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/AutomationPractice/");

        }
        [Test]
        public void SwitchToAlert()
        {
            string enterText = "Shree";
            driver.FindElement(By.Id("name")).SendKeys("Shree");
            driver.FindElement(By.XPath("//input[@id='confirmbtn']")).Click();
            string alertText=driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();
            StringAssert.Contains(enterText, alertText);  

        }
        [Test]
        public void AutoSuggestionDropdown()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("Ind");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("ui-id-1")));
            IList<IWebElement> dropDownElements = driver.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach(IWebElement dropdownElement in dropDownElements)
            {
                
                string dropDownText=dropdownElement.Text;
                if(dropdownElement.Text =="India")
                    dropdownElement.Click();
            }
            string value= driver.FindElement(By.Id("autocomplete")).GetAttribute("value");
            Assert.AreEqual("India", value);

        }
        [Test]
        public void HoverMenuOptions()
        {
            driver.Url = "https://rahulshettyacademy.com/#/index";
            Thread.Sleep(3000);
            Actions action = new Actions(driver);            
            action.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
            action.MoveToElement(driver.FindElement(By.XPath("//li/a[text()='About us']"))).Click().Perform();


        }
        [Test]
        public void MoveToFrame()
        {
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            IJavaScriptExecutor js=(IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(By.Id("courses-iframe")));
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.XPath("//li/a[text()='All Access plan']")).Click();
            string plan=driver.FindElement(By.TagName("h1")).Text;
            StringAssert.Contains("ALL ACCESS SUBSCRIPTION", plan);
            driver.SwitchTo().DefaultContent();
            string defaultPage = driver.FindElement(By.TagName("h1")).Text;
            StringAssert.Contains("Practice Page", defaultPage);
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            /*driver.Quit();*/
            driver.Dispose();
        }
    }
}
