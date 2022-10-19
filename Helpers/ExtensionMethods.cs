using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FrontiersTask.Helpers
{
    public static class ExtensionMethods
    {
        public static void SelectDropdownByValue(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByValue(value);
        }
    }
}
