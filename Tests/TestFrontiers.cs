using FrontiersTask.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontiersTask
{
    [TestFixture(Browser.Chrome)]
    public class TestFrontiers : TestBase
    {
        public TestFrontiers(Browser browser) : base (browser) {}

        IWebElement constructionsDropdown => driver.FindElement(By.CssSelector("#lists-pages-select-container select"));
        IReadOnlyCollection<IWebElement> buildingsTableRows => driver.FindElements(By.CssSelector("#buildingsTable tbody tr"));

        [Test]
        //[TestCase("")]
        public void CountTallest100CompletedBuildingsTest()
        {
            wait.Until(driver => constructionsDropdown);
            constructionsDropdown.SelectDropdownByValue("tallest100-completed");

            var expectedBuildingsCount = 100;

            Assert.AreEqual(expectedBuildingsCount, buildingsTableRows.Count, string.Format("The list contains {0} instead of {1} elemets!", buildingsTableRows.Count, expectedBuildingsCount));
        }

        [Test]
        //[TestCase("")]
        public void LotteWorldTowerFloorsNumberTest()
        {
            wait.Until(driver => constructionsDropdown);
            constructionsDropdown.SelectDropdownByValue("tallest100-completed");

            string expectedFloorsNumber = "123";
            var tagetRowCells = buildingsTableRows.FirstOrDefault(x => x.FindElements(By.TagName("td")).Any(x => x.GetAttribute("data-order") == "Lotte World Tower"));
            var floorsCellValue = tagetRowCells.FindElements(By.TagName("td")).FirstOrDefault(x => x.GetAttribute("class").Contains("forget")).Text;
            
            Assert.AreEqual(expectedFloorsNumber, floorsCellValue, string.Format("The Lotte World Tower building has {0} floors instead of {1}!", buildingsTableRows.Count, expectedFloorsNumber));
        }

        [Test]
        //[TestCase("")]
        public void MaxNumberOfFloorsBuildingTest()
        {
            wait.Until(driver => constructionsDropdown);
            constructionsDropdown.SelectDropdownByValue("tallest100-completed");

            var tagetRowCells = buildingsTableRows.FirstOrDefault(x => x.FindElements(By.TagName("td")).Max(x => x.GetAttribute("class").Contains("forget")));
            //string expectedMaxFloorsBuilding = "Burj Khalifa";
            var maxFloorsBuilding = tagetRowCells.FindElements(By.TagName("td")).FirstOrDefault(x => x.GetAttribute("class").Contains("building-hover")).Text;

            //Assert.AreEqual(expectedMaxFloorsBuilding, maxFloorsBuilding, string.Format("The highest building is {0} instead of {1} !", buildingsTableRows.Count, expectedMaxFloorsBuilding));

            Console.WriteLine(string.Format("The highest building is {0}.", maxFloorsBuilding));
        }
    }
}