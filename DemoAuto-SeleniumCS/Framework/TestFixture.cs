using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumDotNetAutomation.Framework
{
    [TestClass]
    public class TestFixture
    {
        public required IWebDriver Driver;
        public TestContext TestContext { get; set; }

        [AssemblyInitialize]
        public static void BeforeAssembly(TestContext testContext)
        {
            Console.WriteLine("BeforeAssembly...");
        }

        [AssemblyCleanup]
        public static void AfterAssembly()
        {
            Console.WriteLine("AfterAssembly...");
        }

        [ClassInitialize]
        public static void BeforeClass(TestContext testContext)
        {
            Console.WriteLine("BeforeClass...");
        }

        [ClassCleanup]
        public static void AfterClass()
        {
            Console.WriteLine("AfterClass...");
        }

        [TestInitialize]
        public virtual void BeforeTest()
        {
            var options = new ChromeOptions();
            Driver = new ChromeDriver(options);
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TestCleanup]
        public virtual void AfterTest()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}