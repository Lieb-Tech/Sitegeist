using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;
using System;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Actions
{
    public class WaitForVisibilityActionRuntime : BaseActionRuntime, IScriptActionRuntime
    {
        public WaitForVisibilityActionRuntime(WaitForVisibilityActionConfig config) : base(config)
        {
            MaxWait = config.MaxWait;
        }

        /// <summary>
        /// Explict wait time for element
        /// </summary>
        public TimeSpan MaxWait { get; set; }

        public override Dictionary<string, string> Parameters => new Dictionary<string, string>()
        {
            { "MaxWait", MaxWait.ToString() }
        };

        /// <summary>
        /// Wait upto configured time for element to be visible on screen
        /// </summary>
        /// <param name="webDriver">Selenium WebDriver</param>
        public override void PeformAction(IWebDriver webDriver)
        {
            Logger.Log($"Ensure presence: {Target.Path}", MessageTypes.ScriptAction | MessageTypes.Internal);
            Utils.GetElementByPath.GetElement(Target, webDriver);            

            Logger.Log($"Wait until element is visible", MessageTypes.ScriptAction | MessageTypes.Internal);
            var wdw = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wdw.Until(e => SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Target.ToBy()));            
        }
    }
}
