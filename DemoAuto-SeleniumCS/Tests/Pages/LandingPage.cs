using OpenQA.Selenium;
using SeleniumDotNetAutomation.Framework.Utils.SeleniumExtensions;

namespace SeleniumDotNetAutomation.Tests.Pages
{
    public class LandingPage(IWebDriver driver) : BasePage(driver)
    {
        public SearchFlightComponent SearchFlightComponent = new(driver);
        private IWebElement _acceptCookiesBtn => Driver.ExtendedFindElement(By.XPath("//button[.='Accept']"));

        public LandingPage Navigate()
        {
            Driver.Navigate().GoToUrl("https://www.emirates.com/ae/english/");
            return this;
        }

        public LandingPage AcceptCookies()
        {            
            _acceptCookiesBtn.Click();
            return this;
        }

    }
}