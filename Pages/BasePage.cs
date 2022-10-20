using OpenQA.Selenium;

namespace FrontiersTask.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver _driver;

        protected readonly string baseUrl = "https://www.skyscrapercenter.com/buildings?list=tallest100-construction";

        protected BasePage(IWebDriver driver)
        {
            _driver = driver;
        }
    }
}
