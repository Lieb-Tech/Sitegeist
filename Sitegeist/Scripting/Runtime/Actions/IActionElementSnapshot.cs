using OpenQA.Selenium;
using Sitegeist.Scripting.Loggers;

namespace Sitegeist.Scripting.Actions
{
    public interface IActionElementSnapshot
    {
        /// <summary>
        /// Peform the configured action on the target element
        /// </summary>
        /// <param name="WebDriver">Selemium WebDriver</param>
        void TakeElementSnapshot(IWebDriver webDriver, IWebElement element, string path, ILogger logger);
        string ActionSnaphotPath { get; set; }
        bool TakeElementSnaphots { get; set; }
    }
}
