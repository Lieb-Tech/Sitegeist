using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Config.Actions
{
    /// <summary>
    ///  Configuration for pressing SpecialKeys (like space, backspace, enter)
    /// </summary>
    public class SpecialKeyActionConfig : IScriptActionConfig, IHasTarget
    {
        /// <summary>
        /// ID for this action
        /// </summary>
        public string ScriptActionID { get; set; }

        public enum SpecialKeys
        {
            Escape,
            Enter,
            Backspace,            
        }
   
        /// <summary>
        /// Text to enter in the box
        /// </summary>
        public SpecialKeys Key { get; set; }
        /// <summary>
        /// Which element to enter the text into
        /// </summary>
        public IPath Target { get; set; }
    }
}
