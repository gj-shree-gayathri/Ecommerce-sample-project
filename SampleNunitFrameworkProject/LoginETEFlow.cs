using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SampleNunitFrameworkProject.pageObjects;
using SampleNunitFrameworkProject.Utilities;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNunitFrameworkProject
{
   [Parallelizable(ParallelScope.Self)]
    public class LoginETEFlow : Baseclass
    {
        [Test]
        [TestCase("rahulshettyacademy", "learning")]
        [TestCase("rahulshettyacademy", "learn")]
        [Parallelizable(ParallelScope.All)]

        public void ALoginSuccessFlow(string username, string password)
        {
            LoginPage loginPage = new LoginPage(GetDriver());
            ProductsPage products = loginPage.ValidLogin(username, password);
            products.waitForPageTODisplay();
            products.AddProducts();
            CheckoutPage checkOut =products.ClickCheckout();
            List<string> expProducts = new List<string> { "iphone X", "Samsung Note 8" };
            List<string> checkOutItems = checkOut.GetCheckoutItems();
            Assert.That(checkOutItems, Is.EqualTo(expProducts));
            string successMsg=checkOut.CheckOutList();
            StringAssert.AreEqualIgnoringCase("Success!", successMsg);
            Thread.Sleep(5000);

            /*loginPage.GetUsername().SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//input[@value='user']")).Click(); //okayBtn
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(driver => driver.FindElement(By.Id("okayBtn")));
            driver.FindElement(By.Id("okayBtn")).Click();
            IWebElement webElement = driver.FindElement(By.XPath("//Select[@data-style='btn-info']"));
            SelectElement dropdownvalue = new SelectElement(webElement);
            dropdownvalue.SelectByValue("consult");
            driver.FindElement(By.Id("signInBtn")).Click();
            LoginETEFlowTest();
            CheckoutFlow();*/
        }

        /*        [Test]
                public void LoginETEFlowTest()
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

        /*        [Test]*/
        public void CheckoutFlow()
        {
            List<string> expProducts = new List<string> { "iphone X", "Samsung Note 8" };
            List<string> checkOutItems = new List<string>();
            IList<IWebElement> checkOutList = driver.Value.FindElements(By.XPath("//h4//a"));
            foreach (IWebElement element in checkOutList)
            {
                checkOutItems.Add(element.Text);

            }
            Thread.Sleep(5000);
            Assert.That(checkOutItems, Is.EqualTo(expProducts));
            driver.Value.FindElement(By.CssSelector(".btn.btn-success")).Click();
            driver.Value.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();
            driver.Value.FindElement(By.Id("country")).SendKeys("Ind");
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
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.Value.FindElement(By.LinkText("India")).Click();
            driver.Value.FindElement(By.CssSelector("[value='Purchase']")).Click();
            string successText = driver.Value.FindElement(By.CssSelector(".alert-success strong")).Text;
            StringAssert.AreEqualIgnoringCase("Success!", successText);
            Thread.Sleep(5000);

        }


        //Testcase data - same class, separate class
        [Test]
         [TestCaseSource("ValidCredsTest")]
        [Parallelizable(ParallelScope.All)]
        public void ALoginSuccessFlowWithTestCaseSource(string username, string password, List<string> expProd)
         {
             LoginPage loginPage = new LoginPage(GetDriver());
             ProductsPage products = loginPage.ValidLogin(username,password);
             products.waitForPageTODisplay();
             products.AddProducts();
             CheckoutPage checkOut = products.ClickCheckout();
/*             List<string> expProducts = new List<string> { "iphone X", "Samsung Note 8" };
*/             List<string> checkOutItems = checkOut.GetCheckoutItems();
             Assert.That(checkOutItems, Is.EqualTo(expProd));
             string successMsg = checkOut.CheckOutList();
             StringAssert.AreEqualIgnoringCase("Success!", successMsg);
             Thread.Sleep(5000);

             /*loginPage.GetUsername().SendKeys("rahulshettyacademy");
             driver.FindElement(By.Id("password")).SendKeys("learning");
             driver.FindElement(By.XPath("//input[@value='user']")).Click(); //okayBtn
             WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
             wait.Until(driver => driver.FindElement(By.Id("okayBtn")));
             driver.FindElement(By.Id("okayBtn")).Click();
             IWebElement webElement = driver.FindElement(By.XPath("//Select[@data-style='btn-info']"));
             SelectElement dropdownvalue = new SelectElement(webElement);
             dropdownvalue.SelectByValue("consult");
             driver.FindElement(By.Id("signInBtn")).Click();
             LoginETEFlowTest();
             CheckoutFlow();*/
         }

         public static IEnumerable<TestCaseData> ValidCreds()
         {
             yield return new TestCaseData("rahulshettyacademy", "learning");
             yield return new TestCaseData("rahulshettyacademy", "learning1");

         }

        public static IEnumerable<TestCaseData> ValidCredsTest()
        {
            var jsonReader = GetJsonReader();
            yield return new TestCaseData(jsonReader.ExtractData("username"),jsonReader.ExtractData("password"), jsonReader.ExtractArrayData("expProducts"));
            yield return new TestCaseData(jsonReader.ExtractData("username-wrong"), jsonReader.ExtractData("password-wrong"), jsonReader.ExtractArrayData("expProducts"));

        }
    }
}
