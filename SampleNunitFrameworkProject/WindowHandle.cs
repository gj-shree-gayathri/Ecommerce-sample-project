using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SampleNunitFrameworkProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNunitFrameworkProject
{
    [Parallelizable(ParallelScope.Self)]
    public class WindowHandle : Baseclass
    {

        [Test]
        public void WindowHandleCheck()
        {
            Thread.Sleep(15000);
            string currentWindow = GetDriver().CurrentWindowHandle;
            GetDriver().FindElement(By.CssSelector(".blinkingText")).Click();
            var tabs= GetDriver().WindowHandles.ToList();
            string childwindow= GetDriver().WindowHandles[1];
            GetDriver().SwitchTo().Window(childwindow);
            string email=GetDriver().FindElement(By.XPath("//strong//a")).Text;
            GetDriver().SwitchTo().Window(currentWindow);
            GetDriver().FindElement(By.Id("username")).SendKeys(email);
            Assert.True(true);

        }

    }
}
