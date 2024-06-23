using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject
{
    public class Login
    {
        ChromeDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");

        }

        [Test, Order(1)]
        public void ALoginSuccessFlow()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//input[@value='user']")).Click(); //okayBtn
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(driver => driver.FindElement(By.Id("okayBtn")));
            driver.FindElement(By.Id("okayBtn")).Click();
            IWebElement webElement = driver.FindElement(By.XPath("//Select[@data-style='btn-info']"));
            SelectElement dropdownvalue = new SelectElement(webElement);
            dropdownvalue.SelectByValue("consult");
            driver.FindElement(By.Id("signInBtn")).Click();

        }
        [Test, Order(2)]
        public void LoginETEFlow()
        {
            String[] expProducts = { "iphone X", "Samsung Note 8" };
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(driver => driver.FindElement(By.CssSelector(".nav-link.btn.btn-primary")));
            IList<IWebElement> prods = driver.FindElements(By.TagName("app-card"));
            foreach (IWebElement element in prods)
            {
                string txt = element.FindElement(By.CssSelector(".card-title a")).Text;
                if (expProducts.Contains(txt))
                {
                    element.FindElement(By.CssSelector(".card-footer button")).Click();

                }

            }
            driver.FindElement(By.CssSelector(".nav-link.btn.btn-primary")).Click();


        }

        [Test,Order(3)]
        public void CheckoutFlow()
        {
            List<string> expProducts = new List<string>{ "iphone X", "Samsung Note 8" };
            List<string> checkOutItems=new List<string>();
            IList<IWebElement> checkOutList=driver.FindElements(By.XPath("//h4//a"));
            foreach(IWebElement element in checkOutList)
            {
                checkOutItems.Add(element.Text);
                
            }
            Thread.Sleep(5000);
            Assert.That(checkOutItems, Is.EqualTo(expProducts));
            driver.FindElement(By.CssSelector(".btn.btn-success")).Click();
            driver.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();
            driver.FindElement(By.Id("country")).SendKeys("Ind");
            /*WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".suggestions ul li a")));
            IList<IWebElement> list= driver.FindElements(By.CssSelector(".suggestions ul li a"));
             foreach (IWebElement element in list)
             {
                 string country=element.Text;
                 if (country == "India")
                 {
                     wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("India")));

                     element.Click();
                 }

             }*/
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.FindElement(By.LinkText("India")).Click();
            driver.FindElement(By.CssSelector("[value='Purchase']")).Click();
            string successText=driver.FindElement(By.CssSelector(".alert-success strong")).Text;         
            StringAssert.AreEqualIgnoringCase("Success!", successText);
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
