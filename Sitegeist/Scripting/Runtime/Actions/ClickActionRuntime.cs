using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Loggers;
using System;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Actions
{
    public class ClickActionRuntime : BaseActionRuntime, IScriptActionRuntime
    {
        public ClickActionRuntime(ClickActionConfig config) : base(config)
        {
            ExplicitWait = config.ExplicitWait;
        }

        /// <summary>
        /// How long to wait until element is clickable
        /// </summary>
        public TimeSpan ExplicitWait { get; set; }

        /// <summary>
        /// For logging purposes
        /// </summary>
        public override Dictionary<string, string> Parameters => new Dictionary<string, string>()
        {
            { "ExplicitWait", ExplicitWait.ToString() }
        };

        /// <summary>
        /// Execute the click, with optional before after action snapshots
        /// </summary>
        /// <param name="webDriver">Selenium WebDriver</param>
        /// <param name=takeActionSnapshots">takeActionSnapshots</param>
        public override void PeformAction(IWebDriver webDriver)
        {
            Logger.Log($"Get Element: {Target.Path}", MessageTypes.ScriptAction | MessageTypes.Internal);
            var element = Utils.GetElementByPath.GetElement(Target, webDriver);

            Logger.Log($"Perform click", MessageTypes.ScriptAction | MessageTypes.Internal);
            element.Click();
        }
    }
}
