
namespace Sitegeist.Scripting.Config.Expects    
{
    /// <summary>
    ///  Interface for the configuration for expectations
    /// </summary>

    public interface IScriptExpectConfig
    {
        string ScriptExpectID { get; set; }

        /// <summary>
        /// If the expectation is not met, what message should be logged
        /// </summary>
        string Message { get; set; }
    }
}
