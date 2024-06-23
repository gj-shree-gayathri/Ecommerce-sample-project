using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNunitFrameworkProject.pageObjects
{
    public class ProductsPage
    {
        private IWebDriver driver;
        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How=How.CssSelector, Using = ".nav-link.btn.btn-primary")]
        private IWebElement checkOut;
        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> Products;



        public void waitForPageTODisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
/*            wait.Until(driver => checkOut);*/
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".nav-link.btn.btn-primary")));
        }

        public void AddProducts()
        {
            String[] expProducts = { "iphone X", "Samsung Note 8" };

            foreach (IWebElement element in Products)
            {
                string txt = element.FindElement(By.CssSelector(".card-title a")).Text;
                if (expProducts.Contains(txt))
                {
                    element.FindElement(By.CssSelector(".card-footer button")).Click();

                }

            }           
        }

        public CheckoutPage ClickCheckout()
        {
            checkOut.Click();
            return new CheckoutPage(driver);
        }
    }

    
}
