using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Config.Actions
{
    /// <summary>
    ///  Configuration for entering text in an element
    /// </summary>
    public class EnterTextActionConfig : IScriptActionConfig, IHasTarget
    {
        /// <summary>
        /// ID for this action
        /// </summary>
        public string ScriptActionID { get; set; }

        /// <summary>
        /// Output messages
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Element to enter text into
        /// </summary>
        public IPath Target { get; set; }

        /// <summary>
        /// Path & Text combinations to enter
        /// </summary>
        public string Text { get; set; }
    }
}
