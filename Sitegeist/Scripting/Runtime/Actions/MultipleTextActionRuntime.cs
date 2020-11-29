using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Actions
{
    public class MultipleTextActionRuntime : BaseActionRuntime, IInjectable
    {
        public MultipleTextActionRuntime(MultipleTextActionConfig config) : base(config)
        {
            Values = new Dictionary<IPath, string>(config.Values);
        }

        /// <summary>
        /// Path & Text combinations to enter
        /// </summary>
        public Dictionary<IPath, string> Values { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                var ret = new Dictionary<string, string>();

                foreach (var kv in Values)
                {
                    ret.Add(kv.Key.ToString(), kv.Value);
                }
                return ret;
            }
        }

        /// <summary>
        /// Send the text to the respective elements
        /// </summary>
        /// <param name="webDriver">Selenium WebDriver</param>
        public override void PeformAction(IWebDriver webDriver)
        {
            foreach (var value in Values)
            {
                Logger.Log("Getting element: " + value.Key, MessageTypes.ScriptAction | MessageTypes.Internal);
                var element = Utils.GetElementByPath.GetElement(value.Key, webDriver);

                Logger.Log("Sending: " + value.Value, MessageTypes.ScriptAction | MessageTypes.Internal);
                element.SendKeys(value.Value);
            }
        }

        /// <summary>
        /// Replace placeholders with variables
        /// </summary>
        /// <param name="variables">Name/Value dictionary</param>
        public void InjectVariables(IGlobalVariables variables)
        {
            Dictionary<IPath, string> tempValues = new Dictionary<IPath, string>();
            foreach (var value in Values)
            {
                Logger.Log($"Before injection: {value.Key} => {value.Value}", MessageTypes.ScriptAction | MessageTypes.Internal);
                tempValues.Add(value.Key, ValueReplacer.InjectVariables(variables, value.Value));
                Logger.Log($"After injection: {value.Key} => {value.Value}", MessageTypes.ScriptAction | MessageTypes.Internal);
            }
            Values.Clear();
            Values = tempValues;
        }
    }
}
