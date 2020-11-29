using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Sitegeist.Scripting.Paths;
using System;

namespace Sitegeist.Utils
{
    public static class GetElementByPath
    {
        public static IWebElement GetElement(IPath Path, IWebDriver WebDriver)
        {
            IWebElement element = null;
            try
            {
                WebDriverWait wdw = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5));
                wdw.Until(e => SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(Path.ToBy()));
                element = WebDriver.FindElement(Path.ToBy());
            }
            catch (Exception ex)
            {
                if (element is null)
                    throw new ArgumentException($"Path ({Path.Path}) of ({Path.PathType.ToString("G")}) not found");
            }
            return element;
        }
    }
}
