using FrontiersTask.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace FrontiersTask
{
    public class TestBase
    {
        public IWebDriver driver;

        public Browser BrowserName { get; }

        public WebDriverWait wait;

        public TestBase(Browser browser)
        {
            BrowserName = browser;
        }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.skyscrapercenter.com/buildings?list=tallest100-construction");
            driver.FindElement(By.CssSelector(".js-cookie-consent-agree")).Click();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}
