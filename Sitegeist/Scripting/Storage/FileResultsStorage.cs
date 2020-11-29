using Newtonsoft.Json;
using Sitegeist.Scripting.Engine;
using Sitegeist.Utils;
using System.IO;

namespace Sitegeist.Scripting.Storage
{

    /// <summary>
    /// Save/retrive results to file store
    /// </summary>
    public class FileResultsStorage : IResultsStorage
    {
        /// <summary>
        /// Get previous run's results
        /// </summary>
        /// <param name="path">Full filepath</param>
        /// <returns></returns>
        public ScriptRunResults LoadResults(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ScriptRunResults>(json);
        }

        /// <summary>
        /// Save a result of a run
        /// </summary>
        /// <param name="results"></param>
        public void SaveResults(ScriptRunResults results)
        {
            // get settings from configuration file
            var dir = ScriptEngine.ConfigurationSettings.Settings.FileResultsStorage.Folder;
            var file = ScriptEngine.ConfigurationSettings.Settings.FileResultsStorage.FileName;
            var over = ScriptEngine.ConfigurationSettings.Settings.FileResultsStorage.Overwrite;

            // generate the path (make it unique if necessary)
            var fileName = FilePathBuilder.BuildPath(dir, file, over);

            // save the file data
            File.WriteAllText(fileName, JsonConvert.SerializeObject(results));
        }
    }
}
