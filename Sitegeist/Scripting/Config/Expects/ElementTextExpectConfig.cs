using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Config.Expects
{
    /// <summary>
    ///  Configuration for expectation of a element to have text
    /// </summary>
    public class ElementTextExpectConfig : IScriptExpectConfig, IHasTarget
    {
        public string ScriptExpectID { get; set; }

        /// <summary>
        /// How should the text be compared
        /// </summary>
        public StringMatchTypes MatchType { get; set; }

        /// <summary>
        /// what is the expected text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// What element should be compared 
        /// </summary>
        public IPath Target { get; set; }

        /// <summary>
        /// If the expectation is not met, this is the log message
        /// </summary>
        public string Message { get; set; }
    }
}
