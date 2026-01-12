using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using NUnit.Framework;

namespace DemoAutoAppiumCS
{
    [TestFixture]
    public class Tests
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var serverUri = new Uri("http://127.0.0.1:4723/");
            
            var options = new AppiumOptions {
                PlatformName = "Android",
                AutomationName = "UiAutomator2",
                DeviceName = "Appium_Device"
            };

            // Specify the App you want to test (Settings app in this case)
            options.AddAdditionalAppiumOption("appPackage", "com.android.settings");
            options.AddAdditionalAppiumOption("appActivity", ".Settings");

            _driver = new AndroidDriver(serverUri, options);
        }

        [Test]
        public void Test_ClickBattery()
        {
            // Find and click the 'Battery' setting
            var batteryItem = _driver.FindElement(By.XPath("//*[@text='Battery']"));
            batteryItem.Click();

            Assert.That(_driver.PageSource, Does.Contain("Battery"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}