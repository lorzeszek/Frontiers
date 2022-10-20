using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace FrontiersTask.Helpers
{
    public class DriverFactory
    {
        private IWebDriver driver;
        public IWebDriver Create(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    driver = new ChromeDriver();
                    break;
                case Browser.Firefox:
                    driver = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentException("Provided driver:" + browser + " is not supported. Available: chrome, firefox.");
            }
            return driver;
        }
    }
}
