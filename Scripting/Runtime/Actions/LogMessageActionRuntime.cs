using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Actions
{
    public class LogMessageActionRuntime : BaseActionRuntime, IInjectable
    {
        public LogMessageActionRuntime(LogMessageActionConfig config) : base(config)
        {
            Message = config.Message; 
        }

        /// <summary>
        /// Message to save to log
        /// </summary>
        public string Message { get; set; }
        
        public override Dictionary<string, string> Parameters => new Dictionary<string, string>()
        {
            { "Messasge", Message }
        };

        /// <summary>
        /// Write the message to the log
        /// </summary>
        /// <param name="webDriver">Not used</param>
        public override void PeformAction(IWebDriver webDriver)
        {
            Logger.Log(Message, MessageTypes.UserMessage);
        }

        /// <summary>
        /// Replace placeholders with values
        /// </summary>
        /// <param name="variables">Name/Value Dictionary</param>
        public void InjectVariables(IGlobalVariables variables)
        {            
            Logger.Log($"Before injection: {Message}", MessageTypes.ScriptAction | MessageTypes.Internal);
            Message = ValueReplacer.InjectVariables(variables, Message);
            Logger.Log($"After injection: {Message}", MessageTypes.ScriptAction | MessageTypes.Internal);
        }
    }
}
