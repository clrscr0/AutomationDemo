using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeleniumDotNetAutomation.Framework.Utils.SeleniumExtensions
{
    public static class WebdriverExtension
    {
        private static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static string GetItemFromLocalStorage(this IWebDriver driver, string key)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            return (string)js.ExecuteScript($"return localStorage.getItem('{key}')");
        }

        private const string CssSelectorMechanism = "css selector";

        private const string XPathSelectorMechanism = "xpath";

        private const string TagNameMechanism = "tag name";

        private const string LinkTextMechanism = "link text";

        private const string PartialLinkTextMechanism = "partial link text";



        public static void ScrollBy(this IWebDriver driver, int x, int y)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript($"window.scrollBy({x},{y});");
        }

        public static void SetZoom(this IWebDriver driver, int zoomInPercent)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript($"document.body.style.zoom = '{zoomInPercent}%'");
        }
        public static IWebDriver SwitchToIFrame(this IWebDriver driver, string iframe, int addToWaitTimeMS = 0)
        {
            try
            {
                logger.Debug($"Switching to iFrame [{iframe}]...");
                IWebElement frame = driver.ExtendedFindElement(By.XPath($"//iframe[contains(@id, '{iframe}')]"));
                driver.SwitchTo().Frame(frame);
                return driver;
            }
            catch (NoSuchElementException e)
            {
                List<IWebElement> iframes = driver.FindElements(By.TagName("iframe")).ToList();


                throw new NoSuchElementException($"Switchable iframes: {string.Join(", ", iframes.Select(x => x.GetAttribute("id")))}", e);
            }
        }

        /// <summary>
        /// Drag and drop on absolute point from ViewPort
        /// </summary>
        /// <param name="from">Initial coordinate</param>
        /// <param name="to">Final coordinate</param>
        public static void DragAndDropFromViewportOrigin(this IWebDriver driver, Point from, Point to)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice mouse = new PointerInputDevice(PointerKind.Mouse, "default mouse");
            actionBuilder.AddAction(mouse.CreatePointerMove(CoordinateOrigin.Viewport,
                from.X, from.Y, TimeSpan.Zero));
            actionBuilder.AddAction(mouse.CreatePointerDown(MouseButton.Left));
            // ** Insert a pause for 2 seconds using ActionBuilder Pause **
            actionBuilder.AddAction(mouse.CreatePause(TimeSpan.FromSeconds(2)));
            actionBuilder.AddAction(mouse.CreatePointerMove(CoordinateOrigin.Pointer, to.X, to.Y, TimeSpan.Zero));
            actionBuilder.AddAction(mouse.CreatePointerUp(MouseButton.Left));
            ((IActionExecutor)driver).PerformActions(actionBuilder.ToActionSequenceList());
        }

        /// <summary>
        /// Execute click on specific point from top-left of the element from specified origin
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="point">Point from origin that will be clicked</param>
        /// <param name="origin">Coordinate origin from which click point will be offset</param>
        public static void Click(this IWebDriver driver, Point point, CoordinateOrigin origin)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice mouse = new PointerInputDevice(PointerKind.Mouse, "default mouse");

            actionBuilder.AddAction(mouse.CreatePointerMove(origin,
                point.X, point.Y, TimeSpan.Zero));
            actionBuilder.AddAction(mouse.CreatePointerDown(MouseButton.Left));
            actionBuilder.AddAction(mouse.CreatePointerUp(MouseButton.Left));
            ((IActionExecutor)driver).PerformActions(actionBuilder.ToActionSequenceList());
        }

        public static void ScrollToBottom(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public static void ScrollToTop(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0)");
        }

        public enum ElementState
        {
            Present, //element is present but may not be visible
            Visible, //element is visible
            Enabled //element is visible and enable
        }

        public class ExtendedFindConfig
        {
            public ElementState State;
            public int Retries;
            public TimeSpan Timeout;
            public bool ThrowError;
            public bool RefreshPage;

            /// <summary>
            /// Configuration for ExtendedFindElement
            /// </summary>
            /// <param name="state">Wait for ElementState, default=ElementState.Visible</param>
            /// <param name="retries">Number of retries. Default = 1</param>
            /// <param name="timeout">Wait time before throwing an exception or doing another retry, default=TestSetting.LongWait</param>
            /// <param name="throwError">Will throw null if false, default=true</param>
            /// <param name="refreshPage">Will throw null if false, default=true</param>
            public ExtendedFindConfig(ElementState state = ElementState.Visible, int retries = -1, TimeSpan? timeout = null, bool throwError = true, bool refreshPage = false)
            {
                State = state;
                Retries = 1;
                Timeout = DefaultTimeout;
                ThrowError = throwError;
                RefreshPage = refreshPage;
            }
        }

        /// <summary>
        /// Substitute for FindElement with additional options for stability.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="config">Optional configuration of type ExtendedFindConfig</param>
        /// <param name="parent">Optional. If element is being retrieved from DOM of a parent IWebElement</param>
        /// <returns>IWebElement if found; If not found, returns null or error if throwError is set on config.</returns>
        public static IWebElement ExtendedFindElement(this IWebDriver driver, By by, ExtendedFindConfig config = null, IWebElement parent = null)
        {
            if (config == null) config = new ExtendedFindConfig(); //will default values
            //Logger.LogDebug($"ExtendedFindConfig: {config.ToJsonString()}"); //Commented out, should be LogTrace but not available
            return driver.ExtendedFindElement(by, config.State, config.Retries, config.Timeout, config.ThrowError, parent, config.RefreshPage);
        }

        /// <summary>
        /// Find element with retries
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="state">Wait for ElementState, default=ElementState.Visible</param>
        /// <param name="retries">Number of retries. Default = 1</param>
        /// <param name="timeout">Wait time before throwing an exception or doing another retry, default=TestSetting.LongWait</param>
        /// <param name="throwError">Will throw null if false, default=true</param>
        /// <returns></returns>
        private static IWebElement ExtendedFindElement(this IWebDriver driver, By by, ElementState state, int retries, TimeSpan timeout, bool throwError, IWebElement parent, bool refreshPage)
        {
            IWebElement element = null;
            bool breakLoop = false; //if already found, break the do-while loop

            logger.Debug($"IWebElement Context: [{by.Criteria}] using [{by.Mechanism}]");

            while (!breakLoop)
            {
                try
                {
                    DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
                    fluentWait.Timeout = timeout;
                    fluentWait.PollingInterval = new TimeSpan(timeout.Ticks / 4);
                    fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

                    element = fluentWait.Until(_ =>
                    {
                        return parent != null ? parent.FindElement(by) : driver.FindElement(by);
                    });

                    //Force the outer loop when null
                    if (element == null)
                        throw new WebDriverTimeoutException($"Element not found: {by.Mechanism} using {by.Criteria}");

                    switch (state)
                    {
                        case ElementState.Visible:
                            if (!element.Displayed) throw new WebDriverTimeoutException($"Element is not visible: {by.Mechanism} using {by.Criteria}");
                            break;
                        case ElementState.Enabled:
                            if (!element.Enabled) throw new WebDriverTimeoutException($"Element is not enabled: {by.Mechanism} using {by.Criteria}");
                            break;
                        default:
                            break; //default is checking if ElementState.Present
                    }
                    breakLoop = true; //element found or state is achieved so stop loop
                    Thread.Sleep(2000); //short pause to make sure the element is fully loaded in case selenium is too fast
                }
                catch (Exception e)
                {
                    element = null; //reset to null if returned an error
                    retries--;

                    if (refreshPage) driver.Navigate().Refresh();

                    if (retries < 1) //element not found or state is not achieved after retries specified
                    {
                        if (throwError) throw e; //if throwError is specified true, throw error
                        else breakLoop = true; //else break loop without throwing any error
                    }
                }
            }
            return element;
        }

        /// <summary>
        /// Substitute for FindElements with additional options for stability.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="config">Optional configuration of type ExtendedFindConfig</param>
        /// <returns>List of IWebElement if found; If not found, returns null or error if throwError is set on config.</returns>
        public static List<IWebElement> ExtendedFindElements(this IWebDriver driver, By by, ExtendedFindConfig config = null, IWebElement parent = null)
        {
            if (config == null) config = new ExtendedFindConfig();
            return driver.ExtendedFindElements(by, config.State, config.Retries, config.Timeout, config.ThrowError, parent, config.RefreshPage);
        }

        /// <summary>
        /// Find elements with retries
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="state">Wait for ElementState, default=ElementState.Visible</param>
        /// <param name="retries">Number of retries. Default = 1</param>
        /// <param name="timeout">Wait time before throwing an exception or doing another retry, default=TestSetting.LongWait</param>
        /// <param name="throwError">Will throw null if false, default=true</param>
        /// <param name="parent">Optional. If element is being retrieved from DOM of a parent IWebElement</param>
        /// <returns></returns>
        private static List<IWebElement> ExtendedFindElements(this IWebDriver driver, By by, ElementState state, int retries, TimeSpan timeout, bool throwError, IWebElement parent, bool refreshPage)
        {
            List<IWebElement> elements = null;
            bool breakLoop = false; //if already found, break the do-while loop

            logger.Debug($"IWebElement Context: [{by.Criteria}] using [{by.Mechanism}]");

            while (!breakLoop)
            {
                try
                {
                    DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
                    fluentWait.Timeout = timeout;
                    fluentWait.PollingInterval = new TimeSpan(timeout.Ticks / 4);
                    fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

                    elements = fluentWait.Until(_ =>
                    {
                        var found = (parent != null ? parent.FindElements(by) : driver.FindElements(by)).ToList();

                        if (!found.Any()) return found;

                        if (state == ElementState.Visible && !found.First().Displayed || state == ElementState.Enabled && !found.First().Enabled)
                        {
                            found.Clear();
                            return found;
                        }

                        return found;
                    });

                    if (elements.Count == 0)
                        throw new NullReferenceException($"No elements found with locator {by.Mechanism} using {by.Criteria}.");
                    else
                        breakLoop = true; //element found or state is achieved so stop loop

                    Thread.Sleep(2000); //short pause to make sure the element is fully loaded in case selenium is too fast
                }
                catch (Exception e)
                {
                    retries--;

                    if (refreshPage) driver.Navigate().Refresh();

                    if (retries < 1) //element not found or state is not achieved after retries specified
                    {
                        if (throwError) throw e; //if throwError is specified true, throw error
                        else breakLoop = true; //else break loop without throwing any error
                    }
                }
            }

            return elements;
        }

    }
}