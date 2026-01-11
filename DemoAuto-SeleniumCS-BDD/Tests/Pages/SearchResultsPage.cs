using OpenQA.Selenium;

namespace SeleniumDotNetAutomation.Tests.Pages
{
    public class SearchResultsPage (IWebDriver driver) : BasePage (driver)
    {
        public bool AreFlightsDisplayed()
        {
            //@todo
            return true;
        }
    }
}