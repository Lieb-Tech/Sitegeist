using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Config.Expects
{
    /// <summary>
    ///  Configuration for expectation of the url to match a value/pattern
    /// </summary>
    public class PageUrlExpectConfig : IScriptExpectConfig
    {
        /// <summary>
        /// ID 
        /// </summary>
        public string ScriptExpectID { get; set; }

        /// <summary>
        /// How should the URL be examined
        /// </summary>
        public StringMatchTypes MatchType { get; set; }

        /// <summary>
        /// Expected URL value/pattern
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///  What message should be logged if expectation not met
        /// </summary>
        public string Message { get; set; }
      
    }
}