using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Config.Actions
{
    /// <summary>
    ///  Interface for Action Configurations
    /// </summary>
    public interface IScriptActionConfig
    {
        /// <summary>
        /// Unique id for this action, for persistance/logging
        /// </summary>
        string ScriptActionID { get; set; }       
    }
}
