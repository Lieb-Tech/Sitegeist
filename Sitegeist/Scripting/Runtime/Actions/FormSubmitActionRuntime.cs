using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;
using System;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Actions
{
    public class FormSubmitActionRuntime : BaseActionRuntime
    {
        public FormSubmitActionRuntime(FormSubmitActionConfig config) : base(config)
        {
            ExplicitWait = config.ExplicitWait;            
        }
        public TimeSpan ExplicitWait { get; set; }

        public override Dictionary<string, string> Parameters => new Dictionary<string, string>()
        {
            { "ExplicitWait", ExplicitWait.ToString() }
        };

        public override void PeformAction(IWebDriver webDriver)
        {
            Logger.Log($"Get Element: {Target.Path}", MessageTypes.ScriptAction | MessageTypes.Internal);
            var element = Utils.GetElementByPath.GetElement(Target, webDriver);

            Logger.Log($"Perform submit", MessageTypes.ScriptAction | MessageTypes.Internal);
            element.Submit();
        }
    }
}
