using SeleniumDotNetAutomation.Framework;
using SeleniumDotNetAutomation.Tests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumDotNetAutomation.Tests.Tests
{
    [TestClass]
    public class DemoTest : TestFixture
    {

        [TestMethod]
        public void SampleTest()
        {
            var inTwoDays = DateTime.Now.AddDays(2).ToString("dddd, dd MMMM yyyy");
            var inFourDays = DateTime.Now.AddDays(4).ToString("dddd, dd MMMM yyyy");
            LandingPage landingPage = new(Driver);
            landingPage.Navigate().AcceptCookies();
            landingPage.SearchFlightComponent.SelectDepartureAirport("DXB");
            landingPage.SearchFlightComponent.SelectArrivalAirport("MNL");
            landingPage.SearchFlightComponent.SelectDate(inTwoDays);
            landingPage.SearchFlightComponent.SelectDate(inFourDays);
        }

        [TestMethod]
        public void TestSelenium()
        {
            Driver.Navigate().GoToUrl("https://www.emirates.com/ae/english/");
            Driver.Manage().Window.Maximize();
        }
    }
}
