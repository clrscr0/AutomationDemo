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
using static SeleniumDotNetAutomation.Framework.Utils.SeleniumExtensions.WebdriverExtension;

namespace SeleniumDotNetAutomation.Framework.Utils.SeleniumExtensions
{
    public static class WebElementExtension
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void ScrollToView(this IWebElement element)
        {
            var driver = ((IWrapsDriver)element).WrappedDriver;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            logger.Debug("Scrolled to element view.");
        }

        public static IWebElement ExtendedFindElement(this IWebElement element, By by, ExtendedFindConfig config = null)
        {
            var driver = ((IWrapsDriver)element).WrappedDriver;
            return driver.ExtendedFindElement(by, config, element);
        }

        public static List<IWebElement> ExtendedFindElements(this IWebElement element, By by, ExtendedFindConfig config = null)
        {
            var driver = ((IWrapsDriver)element).WrappedDriver;
            return driver.ExtendedFindElements(by, config, element);
        }

        public static bool HasAttribute(this IWebElement element, string attribute, string value)
        {
            try
            {
                logger.Debug($"Element [@{attribute}: {value}");
                return element.GetAttribute(attribute).Contains(value);
            }
            catch (NoSuchElementException e)
            {
                logger.Debug($"Element does not have the attribute value: {e.Message}");
                return false;
            }
        }
    }
}