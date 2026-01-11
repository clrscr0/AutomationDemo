using Reqnroll;
using OpenQA.Selenium;

namespace Framework
{
    [Binding]
    public class Hooks
    {
        private readonly IWebDriver _driver;

        // The driver is automatically injected by Reqnroll/MS.DI
        public Hooks(IWebDriver driver)
        {
            _driver = driver;
        }

        [BeforeScenario]
        public void Setup()
        {
            _driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void Teardown()
        {
            // The container will dispose of the driver, 
            // but we call Quit() to close the browser process.
            _driver.Quit();
        }
    }
}