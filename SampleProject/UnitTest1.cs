using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SampleProject
{
    public class Tests
    {
        ChromeDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();


        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://google.com");

        }

        [TearDown]
        public void TestTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}