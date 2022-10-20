using FrontiersTask.Helpers;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Linq;

namespace FrontiersTask.Pages
{
    public class MainPage : BasePage
    {
        private IWebElement CookieNotificationButton => _driver.FindElement(By.CssSelector(".js-cookie-consent-agree"),2);
        private IWebElement ConstructionsDropdown => _driver.FindElement(By.CssSelector("#lists-pages-select-container select"),2);
        private ReadOnlyCollection<IWebElement> BuildingsTableRows => _driver.FindElements(By.CssSelector("#buildingsTable tbody tr"),5);
        private ReadOnlyCollection<IWebElement> TableHeaderColumns => _driver.FindElements(By.CssSelector("#buildingsTable thead th"),5);

        private IWebElement TargetBuilding { get; set; }

        public int BuildingNameColumnIndex { get; set; }
        public int CityColumnIndex { get; set; }
        public int FloorsColumnIndex { get; set; }
        public bool IsTargetBuildingOnTheList { get; set; }

        public MainPage(IWebDriver driver) : base(driver) { }

        public MainPage GoTo()
        {
            _driver.Navigate().GoToUrl(baseUrl);
            return this;
        }

        public MainPage CloseCookieNotification()
        {
            CookieNotificationButton.Click();
            return this;
        }

        public MainPage SelectTallest100BuildingsFilter()
        {
            ConstructionsDropdown.SelectDropdownByValue("tallest100-completed");
            return this;
        }

        public int GetBuildingsCount()
        {
            return BuildingsTableRows.Count;
        }

        public MainPage GetBuildingNameColumnIndex()
        {
            BuildingNameColumnIndex = GetColumnIndex("NAME");
            return this;
        }

        public MainPage GetCityColumnIndex()
        {
            CityColumnIndex = GetColumnIndex("CITY");
            return this;
        }

        public MainPage GetFloorsColumnIndex()
        {
            FloorsColumnIndex = GetColumnIndex("FLOORS");
            return this;
        }

        public int GetColumnIndex(string columnName)
        {
            return TableHeaderColumns.IndexOf(TableHeaderColumns.FirstOrDefault(x => x.Text == columnName));
        }

        public MainPage CheckIfTargetBuildingExistsOnTheList(string buildingName, string city)
        {
            TargetBuilding = BuildingsTableRows.FirstOrDefault(x => x.FindElements(By.TagName("td"),5).ElementAt(BuildingNameColumnIndex).Text == buildingName && x.FindElements(By.TagName("td"),5).ElementAt(CityColumnIndex).Text == city);
            IsTargetBuildingOnTheList = TargetBuilding != null;

            return this;
        }

        public string GetBuildingFloorsNumber()
        {
            return TargetBuilding?.FindElements(By.TagName("td")).ElementAt(FloorsColumnIndex)?.Text ?? string.Empty;
        }

        public string GetBuildingWithMaxFloors()
        {
            var targetBuilding = BuildingsTableRows.FirstOrDefault(x => x.FindElements(By.TagName("td"),5).Max(x => x.GetAttribute("class").Contains("forget")));
            return targetBuilding.FindElements(By.TagName("td"),5).FirstOrDefault(x => x.GetAttribute("class").Contains("building-hover")).Text;
        }
    }
}
