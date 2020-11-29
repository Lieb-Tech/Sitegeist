using Sitegeist.Scripting.Loggers;

namespace Sitegeist.Scripting.Config.Getters
{
    /// <summary>
    /// Configuration for getting values from the page
    /// </summary>
    public interface IGetterConfig
    {       
        /// <summary>
        /// The name of the variable to save value to
        /// </summary>
        string VariableName { get; set; }
    }
}
