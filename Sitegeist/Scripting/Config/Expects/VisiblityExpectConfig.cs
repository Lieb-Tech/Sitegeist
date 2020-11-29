using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Config.Expects
{
    /// <summary>
    ///  Configuration for expectation of a element to be visible
    /// </summary>
    public class VisiblityExpectConfig : IScriptExpectConfig, IHasTarget
    {
        /// <summary>
        /// Expect identifier
        /// </summary>
        public string ScriptExpectID { get; set; }
        /// <summary>
        /// Should the target be visible?
        /// </summary>
        public bool ShouldBeVisible { get; set; }
        /// <summary>
        /// What element should be examined
        /// </summary>
        public IPath Target {get;set;}

        /// <summary>
        /// If the expectation is not met, this is the logging message
        /// </summary>
        public string Message { get; set; }
    }
}
