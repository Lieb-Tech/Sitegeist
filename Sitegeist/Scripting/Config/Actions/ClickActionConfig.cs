using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Paths;
using System;

namespace Sitegeist.Scripting.Config.Actions
{
    /// <summary>
    ///  Configuration settings for Clicking on an element
    /// </summary>
    public class ClickActionConfig : IScriptActionConfig, IHasTarget
    {
        /// <summary>
        /// ID for this action
        /// </summary>
        public string ScriptActionID { get; set; }
        /// <summary>
        /// Log out status messages
        /// </summary>
        public TimeSpan ExplicitWait { get; set; }
        /// <summary>
        /// Which element on the page will be clicked
        /// </summary>
        public IPath Target { get; set; }
    }
}
