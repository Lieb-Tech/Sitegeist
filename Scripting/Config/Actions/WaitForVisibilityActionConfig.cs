using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;
using System;

namespace Sitegeist.Scripting.Config.Actions
{
    /// <summary>
    ///  Configuration for waiting for an element to become invisible
    /// </summary>
    public class WaitForVisibilityActionConfig : IScriptActionConfig, IHasTarget
    {
        /// <summary>
        /// ID for this action
        /// </summary>
        public string ScriptActionID { get; set; }

        /// <summary>
        /// Output log messages
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Which element to wait for
        /// </summary>
        public IPath Target { get; set; }

        /// <summary>
        /// Explict wait time for element
        /// </summary>
        public TimeSpan MaxWait { get; set; }
    }
}
