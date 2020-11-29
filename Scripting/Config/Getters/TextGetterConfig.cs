using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Config.Getters
{
    /// <summary>
    /// Configuration for getting text from page & save to variable
    /// </summary>
    public class TextGetterConfig : IGetterConfig, IHasTarget
    {
        /// <summary>
        /// Where to send log information to
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// Which element should text be retrieved from
        /// </summary>
        public IPath Target { get; set; }
        /// <summary>
        /// The name of the global variable to store value for future use
        /// </summary>
        public string VariableName { get; set; }    
    }
}
