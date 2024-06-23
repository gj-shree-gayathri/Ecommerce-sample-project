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
    public class LoginPage
    {
        /*driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
        driver.FindElement(By.Id("password")).SendKeys("learning");
        driver.FindElement(By.XPath("//input[@value='user']")).Click(); //okayBtn*/
        private IWebDriver driver;
        public LoginPage(IWebDriver driver) {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How=How.Id, Using = "username")]
        private IWebElement username;
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;
        [FindsBy(How = How.XPath, Using = "//input[@value='user']")]
        private IWebElement type;
        [FindsBy(How = How.Id, Using = "okayBtn")]
        private IWebElement OkyButton;
        [FindsBy(How = How.Id, Using = "signInBtn")]
        private IWebElement SignInButton;
        [FindsBy(How = How.XPath, Using = "//Select[@data-style='btn-info']")]
        private IWebElement DropDown;


        public IWebElement GetUsername()
        {
            return username;
        }

        public IWebElement GetPassword()
        {
            return password;
        }


        public ProductsPage ValidLogin(string user, string pwd)
        {
            username.SendKeys(user);
            password.SendKeys(pwd);
            type.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(driver => OkyButton);
            OkyButton.Click();
            SelectElement dropdownvalue = new SelectElement(DropDown);
            dropdownvalue.SelectByValue("consult");
            SignInButton.Click();
            return new ProductsPage(driver);

        }
    }
}
