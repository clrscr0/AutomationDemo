using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumDotNetAutomation.Framework.Utils.SeleniumExtensions;
using SeleniumDotNetAutomation.Tests.Pages;

public class SearchFlightComponent(IWebDriver driver)
{
    private IWebElement _tabPanel => driver.FindElement(By.XPath("//*[@aria-labelledby='tab0']"));
    private IWebElement _depCodeField => _tabPanel.ExtendedFindElement(By.Name("Departure airport"));
    private IWebElement _arrCodeField => _tabPanel.ExtendedFindElement(By.Name("Arrival airport"));

    private IWebElement _depDatePicker => _tabPanel.FindElement(By.XPath("//input[@id='search-flight-date-picker--depart']"));

    private IWebElement _searchFlightBtn => _tabPanel.FindElement(By.XPath("//button[./span[.='Search flights']]"));

    public void SelectDepartureAirport(string depCode)
    {
        _depCodeField.Clear();
        _depCodeField.SendKeys(depCode);
        var _depCodeOption = _tabPanel.ExtendedFindElement(By.XPath($"//*[@data-aria-search-id='search-flight-departure']//p[.='{depCode}']"));
        _depCodeOption.Click();
    }

    public void SelectArrivalAirport(string arrCode)
    {
        _arrCodeField.Clear();
        _arrCodeField.SendKeys(arrCode);
        var _arrCodeOption = _tabPanel.ExtendedFindElement(By.XPath($"//*[@data-aria-search-id='search-flight-arrival']//p[.='{arrCode}']"));
        _arrCodeOption.Click();
    }

    public void SelectDate(int daysFromNow)
    {
        var dateVal = DateTime.Now.AddDays(daysFromNow).ToString("dddd, dd MMMM yyyy");
        var dateToSelect = driver.FindElement(By.XPath($"//button[@aria-label='{dateVal}']"));
        dateToSelect.ScrollToView();
        dateToSelect.Click();
    }

    public SearchResultsPage ClickSearchFlight()
    {
        _searchFlightBtn.Click();
        return new SearchResultsPage(driver);
    }

}