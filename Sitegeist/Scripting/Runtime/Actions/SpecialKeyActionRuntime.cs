using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Loggers;
using System.Collections.Generic;
using static Sitegeist.Scripting.Config.Actions.SpecialKeyActionConfig;

namespace Sitegeist.Scripting.Runtime.Actions
{
    public class SpecialKeyActionRuntime : BaseActionRuntime
    {
        public SpecialKeyActionRuntime(SpecialKeyActionConfig config) : base(config)
        {
            Key = config.Key;
        }
      
        /// <summary>
        /// Text to enter in the box
        /// </summary>
        public SpecialKeys Key { get; set; }

        public override Dictionary<string, string> Parameters => new Dictionary<string, string>()
        {
            { "Key", Key.ToString() }
        };

        /// <summary>
        /// Enter the text into the field
        /// </summary>
        /// <param name="webDriver">Selenium WebDriver</param>
        public override void PeformAction(IWebDriver webDriver)
        {
            Logger.Log($"Get Element: {Target.Path}", MessageTypes.ScriptAction | MessageTypes.Internal);
            IWebElement element = Utils.GetElementByPath.GetElement(Target, webDriver);

            Logger.Log($"Enter the Special Key: {Key}", MessageTypes.ScriptAction | MessageTypes.Internal);

            if (Key ==  SpecialKeys.Escape)
                element.SendKeys(OpenQA.Selenium.Keys.Escape);
            else if (Key == SpecialKeys.Backspace)
                element.SendKeys(OpenQA.Selenium.Keys.Backspace);
            else if (Key == SpecialKeys.Enter)
                element.SendKeys(OpenQA.Selenium.Keys.Return);
        }
    }
}
