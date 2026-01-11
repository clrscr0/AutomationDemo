using Reqnroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumDotNetAutomation.Tests.Pages;

namespace SeleniumDotNetAutomation.Tests.Steps
{

    [Binding]
    public class DemoSteps(IWebDriver driver)
    {
        private LandingPage _landingPage;
        private SearchResultsPage _searchResultsPage;
        private readonly IWebDriver _driver = driver;

        [Given(@"I am on the landing page")]
        public void GivenIAmOnTheFlightBookingWebsite() {
            _landingPage = new(_driver);
            _landingPage.Navigate();
            _landingPage.AcceptCookies();
        }
        [When(@"I enter ""(.*)"" as the departure city")]
        public void WhenIEnterDepartureCity(string airportCode) {
            _landingPage.SearchFlightComponent.SelectDepartureAirport(airportCode);
        }

        [When(@"I enter ""(.*)"" as the arrival city")]
        public void WhenIEnterArrivalCity(string airportCode) {
            _landingPage.SearchFlightComponent.SelectArrivalAirport(airportCode);
        }

        [When(@"I select a departure date ""(.*)"" days from now")]
        public void WhenISelectDepartureDate(int daysFromNow)
        {
            _landingPage.SearchFlightComponent.SelectDate(daysFromNow);
        }

        [When(@"I select a return date ""(.*)"" days from now")]
        public void WhenISelectReturnDate(int daysFromNow)
        {
            _landingPage.SearchFlightComponent.SelectDate(daysFromNow);
        }

        [When(@"I click on the Search Flight button")]
        public void WhenIClickOnTheSearchButton() {
            _searchResultsPage = _landingPage.SearchFlightComponent.ClickSearchFlight();
        }

        [Then(@"I should see a list of available flights for ""(.*)"" to ""(.*)""")]
        public void ThenIShouldSeeAListOfFlights(string from, string to) {
            Assert.IsTrue(_searchResultsPage.AreFlightsDisplayed());
        }
    }
}