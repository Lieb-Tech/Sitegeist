using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Paths;
using System;

namespace Sitegeist.Scripting.Config.Actions
{
    /// <summary>
    /// Submit an HTML form
    /// </summary>
    public class FormSubmitActionConfig : IScriptActionConfig, IHasTarget
    {
        /// <summary>
        /// ID for this action
        /// </summary>
        public string ScriptActionID { get; set; }
        /// <summary>
        /// Form target 
        /// </summary>
        public IPath Target { get; set; }
        /// <summary>
        /// How long to wait for Target to be interactable
        /// </summary>
        public TimeSpan ExplicitWait { get; set; }

    }
}
