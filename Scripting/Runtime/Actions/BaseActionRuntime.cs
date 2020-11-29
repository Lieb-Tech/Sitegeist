using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Actions
{
    
    ///  Base class for Actions in runtime form; defines properties and method stubs    
    public abstract class BaseActionRuntime : IScriptActionRuntime, IScriptActionConfig, IHasTarget
    {
        /// <summary>
        /// No default constructor by design -- an runtime action is built from configuration
        /// </summary>
        /// <param name="config">Configuration object ; it can also impliment IHasTarget</param>
        protected BaseActionRuntime(IScriptActionConfig config)
        {            
            ScriptActionID = config.ScriptActionID;

            // if config has Target path, then apply here as well
            if (config is IHasTarget targetConfig)
                Target = targetConfig.Target;
        }

        public ILogger Logger { get; set; }

        public string ScriptActionID { get; set; }
        public IPath Target { get; set; }

        public virtual Dictionary<string, string> Parameters => throw new System.NotImplementedException();

        public virtual void PeformAction(IWebDriver webDriver)
        {
            throw new System.NotImplementedException();
        }
    }
}
