using FrontiersTask.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
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

        public int BuildingNameColumnIndex => GetColumnIndex("NAME");
        public int CityColumnIndex => GetColumnIndex("CITY");
        public int FloorsColumnIndex => GetColumnIndex("FLOORS");
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

        public void VerifyBuildingsCount(string expectedBuildingCount)
        {
            int buildingsCount = BuildingsTableRows.Count;

            Assert.AreEqual(expectedBuildingCount, buildingsCount.ToString(), string.Format("The list contains {0} instead of {1} elemets!", buildingsCount, expectedBuildingCount));
        }

        public int GetColumnIndex(string columnName)
        {
            return TableHeaderColumns.IndexOf(TableHeaderColumns.FirstOrDefault(x => x.Text == columnName));
        }

        public MainPage CheckIfTargetBuildingExistsOnTheList(string buildingName, string city)
        {
            if (BuildingNameColumnIndex > 0 && CityColumnIndex > 0)
            {
                TargetBuilding = BuildingsTableRows.FirstOrDefault(x => x.FindElements(By.TagName("td"), 5).ElementAt(BuildingNameColumnIndex).Text == buildingName && x.FindElements(By.TagName("td"), 5).ElementAt(CityColumnIndex).Text == city);
                IsTargetBuildingOnTheList = TargetBuilding != null;

                Assert.IsTrue(IsTargetBuildingOnTheList, "There building with name: {0}, in the city: {1} was not found on the list!", buildingName, city);

                return this;
            }
            else
            {
                throw new NoSuchElementException("There is no column with Building Name or Column with City!");
            }
        }

        public void VerifyBuildingFloorsNumber(string expectedFloorsNumber)
        {
            if (FloorsColumnIndex > 0)
            {
                string targetBuildingFloors = TargetBuilding?.FindElements(By.TagName("td")).ElementAt(FloorsColumnIndex)?.Text;

                Assert.AreEqual(expectedFloorsNumber, targetBuildingFloors, string.Format("The Lotte World Tower building has {0} floors instead of {1}!", targetBuildingFloors, expectedFloorsNumber));
            }
            else
            {
                throw new NoSuchElementException("There is no column with Floors number!");
            }
        }

        public void GetBuildingWithMaxFloors()
        {
            var targetBuilding = BuildingsTableRows.FirstOrDefault(x => x.FindElements(By.TagName("td"),5).Max(x => x.GetAttribute("class").Contains("forget")));

            if (FloorsColumnIndex > 0)
            {
                string mostFloorsNumber = targetBuilding.FindElements(By.TagName("td")).ElementAt(FloorsColumnIndex)?.Text;

                string maxFloorsBuildingName = targetBuilding.FindElements(By.TagName("td"), 5).FirstOrDefault(x => x.GetAttribute("class").Contains("building-hover")).Text;

                Console.WriteLine(string.Format("The building with the most floors number is {0}, with {1} floors.", maxFloorsBuildingName, mostFloorsNumber));
            }
            else
            {
                throw new NoSuchElementException("There is no column with Floors number!");
            }
        }
    }
}
