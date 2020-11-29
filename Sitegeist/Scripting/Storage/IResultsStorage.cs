using Sitegeist.Scripting.Engine;

namespace Sitegeist.Scripting.Storage
{
    /// <summary>
    /// Interface for storing engine run results 
    /// </summary>
    public interface IResultsStorage
    {
    
        /// <summary>
        /// Persist the results to the storage
        /// </summary>
        /// <param name="results"></param>
        void SaveResults(ScriptRunResults results);

        /// <summary>
        /// Retrieve prior run results
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        ScriptRunResults LoadResults(string path);
    }
}
