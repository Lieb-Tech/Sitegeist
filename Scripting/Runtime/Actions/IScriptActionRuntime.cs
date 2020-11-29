using OpenQA.Selenium;
using Sitegeist.Scripting.Loggers;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Actions
{
    public interface IScriptActionRuntime
    {
        /// <summary>
        /// Peform the configured action on the target element
        /// </summary>
        /// <param name="WebDriver">Selemium WebDriver</param>
        void PeformAction(IWebDriver webDriver);

        /// <summary>
        /// Assign the Logger 
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// Debugging output of the Action's settings
        /// </summary>
        Dictionary<string, string> Parameters { get; }

        /// <summary>
        /// Unique id for this action, for persistance/logging
        /// </summary>
        string ScriptActionID { get; set; }        
    }
}
