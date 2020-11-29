using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Actions
{
    public class EnterTextActionRuntime : BaseActionRuntime, IScriptActionRuntime, IInjectable
    {
        public EnterTextActionRuntime(EnterTextActionConfig config) : base(config)
        {
            Text = config.Text;
        }

        /// <summary>
        /// Path & Text combinations to enter
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///  Modules settings For logging
        /// </summary>
        public override Dictionary<string, string> Parameters
        {
            get
            {
                var ret = new Dictionary<string, string>()
                {
                    {  "Text", Text }
                };                
                return ret;
            }
        }

        /// <summary>
        /// Send the text to the respective elements
        /// </summary>
        /// <param name="webDriver">Selenium WebDriver</param>
        public override void PeformAction(IWebDriver webDriver)
        {
            Logger.Log("Getting element: " + Target, MessageTypes.ScriptAction | MessageTypes.Internal);
            var element = Utils.GetElementByPath.GetElement(Target, webDriver);

            Logger.Log("Sending: " + Text, MessageTypes.ScriptAction | MessageTypes.Internal);
            element.SendKeys(Text);
        }

        /// <summary>
        /// Replace placeholders with variables
        /// </summary>
        /// <param name="variables">Name/Value dictionary</param>
        public void InjectVariables(IGlobalVariables variables)
        {
            Logger.Log($"Before injection: Text => {Text}", MessageTypes.ScriptAction | MessageTypes.Internal);
            Text = ValueReplacer.InjectVariables(variables, Text);
            Logger.Log($"After injection: Text => {Text}", MessageTypes.ScriptAction | MessageTypes.Internal);
        }
    }
}
