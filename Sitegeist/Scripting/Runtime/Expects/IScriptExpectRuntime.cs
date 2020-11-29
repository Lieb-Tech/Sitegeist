using OpenQA.Selenium;
using Sitegeist.Scripting.Paths;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Expects
{
    public interface IScriptExpectRuntime
    {
        /// <summary>
        /// Unique ID for expect
        /// </summary>
        string ScriptExpectID { get; set; }

        /// <summary>
        /// Which element to perform action on
        /// </summary>
        IPath Target { get; set; }

        string Message { get; set; }

        bool CheckExpectation(IWebDriver webDriver);

        Dictionary<string,string> Parameters { get; }
    }
}
