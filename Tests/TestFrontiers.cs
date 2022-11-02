using FrontiersTask.Helpers;
using NUnit.Framework;

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
            _mainPage
            .VerifyBuildingsCount(expectedBuildingCount);
        }

        [Test]
        [TestCase("Lotte World Tower", "Seoul", "123")]
        public void VerifyBuildingFloorNumbers(string buildingName, string city, string expectedNumberOfFloors)
        {
            _mainPage
            .CheckIfTargetBuildingExistsOnTheList(buildingName, city)
            .VerifyBuildingFloorsNumber(expectedNumberOfFloors);
        }

        [Test]
        public void GetMaxFloorsBuildingTest()
        {
            _mainPage
            .GetBuildingWithMaxFloors();
        }
    }
}