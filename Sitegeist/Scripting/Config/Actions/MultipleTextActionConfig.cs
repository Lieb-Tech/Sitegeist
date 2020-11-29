using Sitegeist.Scripting.Paths;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Config.Actions
{
    /// <summary>
    ///  Configuration for entering text in multiple fields
    /// </summary>
    public class MultipleTextActionConfig : IScriptActionConfig
    {
        /// <summary>
        /// ID for this action
        /// </summary>
        public string ScriptActionID { get; set; }
       

        /// <summary>
        /// Path & Text combinations to enter
        /// </summary>
        public Dictionary<IPath, string> Values { get; set; }


    }
}
