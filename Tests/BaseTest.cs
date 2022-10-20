using FrontiersTask.Helpers;
using FrontiersTask.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace FrontiersTask
{
    public class BaseTest
    {
        protected IWebDriver _driver;
        protected Browser _browser;
        protected BaseTest(Browser browser)
        {
            _browser = browser;
        }

        [SetUp]
        public void Setup()
        {
            _driver = new DriverFactory().Create(_browser);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            _driver.Manage().Window.Maximize();

            MainPage mainPage = new MainPage(_driver);
            mainPage.GoTo().CloseCookieNotification().SelectTallest100BuildingsFilter();
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}
