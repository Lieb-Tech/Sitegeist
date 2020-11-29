using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Expects;
using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Paths;
using System;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Expects
{
    /// Base runtime for expectations
    public abstract class BaseExpectRuntime : IScriptExpectRuntime, IScriptExpectConfig
    {
        /// <summary>
        /// No default constructor, as configuration should be passed in
        /// </summary>
        /// <param name="config"></param>
        protected BaseExpectRuntime(IScriptExpectConfig config)
        {
            ScriptExpectID = config.ScriptExpectID;            
            Message = config.Message;

            if (config is IHasTarget targetConfig)
                Target = targetConfig.Target;
        }
        public string ScriptExpectID { get; set; }
        public IPath Target { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Settings for logging output
        /// </summary>
        public virtual Dictionary<string, string> Parameters => throw new NotImplementedException();

        /// <summary>
        /// Execute the expection check
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public virtual bool CheckExpectation(IWebDriver webDriver)
        {
            throw new NotImplementedException();
        }
    }
}
