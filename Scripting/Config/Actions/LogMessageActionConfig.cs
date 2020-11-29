using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Config.Actions
{
    /// <summary>
    ///  Configuration for writing information to the log
    /// </summary>
    public class LogMessageActionConfig : IScriptActionConfig, IHasTarget
    {
        /// <summary>
        /// ID for this action
        /// </summary>
        public string ScriptActionID { get; set; }
  
        /// <summary>
        /// Message to save to log
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Not used for logging
        /// </summary>
        public IPath Target { get; set; }   
    }
}
