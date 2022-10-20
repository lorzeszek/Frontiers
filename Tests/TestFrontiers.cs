using FrontiersTask.Helpers;
using FrontiersTask.Pages;
using NUnit.Framework;
using System;

namespace FrontiersTask
{
    [TestFixture(Browser.Firefox)]
    public class TestFrontiers : BaseTest
    {
        public TestFrontiers(Browser browser) : base (browser) {}

        [Test]
        [TestCase("100")]
        public void VerifyCompletedBuildingsCountTest(string expectedBuildingCount)
        {
            var mainPage = new MainPage(_driver);

            var buildingsCount = mainPage.GetBuildingsCount().ToString();

            Assert.AreEqual(expectedBuildingCount, buildingsCount, string.Format("The list contains {0} instead of {1} elemets!", buildingsCount, expectedBuildingCount));
        }

        [Test]
        [TestCase("Lotte World Tower", "Seoul", "123")]
        public void VerifyBuildingFloorNumbers(string buildingName, string city, string expectedNumberOfFloors)
        {
            var mainPage = new MainPage(_driver);

            string targetBuildingFloorsCount = mainPage
                .GetCityColumnIndex()
                .GetBuildingNameColumnIndex()
                .GetFloorsColumnIndex()
                .CheckIfTargetBuildingExistsOnTheList(buildingName, city)
                .GetBuildingFloorsNumber();

            Assert.Multiple(() =>
            {
                Assert.Positive(mainPage.CityColumnIndex, "There is no column with label {0}!", "CITY");
                Assert.Positive(mainPage.BuildingNameColumnIndex, "There is no column with label {0}!", "NAME");
                Assert.Positive(mainPage.FloorsColumnIndex, "There is no column with label {0}!", "FLOORS");
                Assert.IsTrue(mainPage.IsTargetBuildingOnTheList, "There building with name: {0}, in the city: {1} was not found on the list!", buildingName, city);
                Assert.AreEqual(expectedNumberOfFloors, targetBuildingFloorsCount, string.Format("The Lotte World Tower building has {0} floors instead of {1}!", targetBuildingFloorsCount, expectedNumberOfFloors));
            });

        }

        [Test]
        public void GetMaxFloorsBuildingTest()
        {
            var mainPage = new MainPage(_driver);
            string maxFloorsBuildingName = mainPage
                .GetCityColumnIndex()
                .GetBuildingNameColumnIndex()
                .GetFloorsColumnIndex()
                .GetBuildingWithMaxFloors();

            Console.WriteLine(string.Format("The building with the most floors number is {0}.", maxFloorsBuildingName));
        }
    }
}