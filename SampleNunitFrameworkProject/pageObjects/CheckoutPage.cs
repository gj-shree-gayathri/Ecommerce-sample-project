using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNunitFrameworkProject.pageObjects
{
    public class CheckoutPage
    {
        private IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//h4//a")]
        private IList<IWebElement> checkOutList;

        [FindsBy(How = How.CssSelector, Using = ".btn.btn-success")]
        private IWebElement purchase;

        [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
        private IWebElement checkBox;

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement Countryfield;

        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement SearchResult;

        [FindsBy(How = How.CssSelector, Using = "[value='Purchase']")]
        private IWebElement PurchaseButton;

        [FindsBy(How = How.CssSelector, Using = ".alert-success strong")]
        private IWebElement SuccessMsg;


        public List<string> GetCheckoutItems()
        {
            List<string> checkOutItems = new List<string>();
            foreach (IWebElement element in checkOutList)
            {
                checkOutItems.Add(element.Text);

            }
            return checkOutItems;

        }

        public string CheckOutList()
        {
            purchase.Click();
            checkBox.Click();
            Countryfield.SendKeys("Ind");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            SearchResult.Click();
            PurchaseButton.Click();
            return SuccessMsg.Text;
            

        }
    }
}
