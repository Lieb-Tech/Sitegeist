using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Getters;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;
using System;

namespace Sitegeist.Scripting.Runtime.Getters
{
    public class TextGetterRuntime :BaseGetterRuntime,  IGetterRuntime
    {
        public TextGetterRuntime(TextGetterConfig config) :base(config)
        {

        }
        
        public override string GetValue(IWebDriver webDriver)
        {
            Logger.Log($"Get Element: {Target.Path}", MessageTypes.ScriptAction | MessageTypes.Internal);
            var element = Utils.GetElementByPath.GetElement(Target, webDriver);

            return element.Text;
        }
    }
}
