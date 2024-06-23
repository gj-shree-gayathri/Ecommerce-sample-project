using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject
{
    public class SortColumn
    {
        ChromeDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/seleniumPractise/#/offers");

        }

        [Test]
        public void Sort()
        {
            IWebElement element = driver.FindElement(By.Id("page-menu"));
            SelectElement dropDown = new SelectElement(element);
            dropDown.SelectByValue("20");
            ArrayList A = new ArrayList();
            IList<IWebElement> column=driver.FindElements(By.XPath("//tr//td[1]"));
            foreach (IWebElement elementvalue in column)
            {
                A.Add(elementvalue.Text);
            }
            A.Sort();
            driver.FindElement(By.XPath("//tr//th[1]")).Click();
            ArrayList B = new ArrayList();
            IList<IWebElement> aftersort = driver.FindElements(By.XPath("//tr//td[1]"));
            foreach (IWebElement value in aftersort)
            {
                B.Add(value.Text);
            }
            Assert.AreEqual(A, B);





        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            /*driver.Quit();*/
            driver.Dispose();
        }

    }
}
