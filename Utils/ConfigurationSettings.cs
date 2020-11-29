using Newtonsoft.Json;
using System.IO;

namespace Sitegeist.Utils
{
    public class ConfigurationSettings
    {
        public ApplicationSettings Settings { get; private set; }

        public ConfigurationSettings()
        {
            var json = File.ReadAllText("settings.json");
            Settings = JsonConvert.DeserializeObject<ApplicationSettings>(json);
        }
    }

    public class ApplicationSettings
    {
        public FileLoggerSettings FileLoggerSettings { get; set; }
        public FileResultsStorage FileResultsStorage { get; set; }
    }

    public class FileLoggerSettings {
        /// <summary>
        ///  Required - path to store log files
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// Optional - Filename for logs. If not provided, default value will be used
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Optional - If file exists, should it be overwritten each run? If not provided, default value will be used (false)
        /// </summary>
        public bool Overwrite { get; set; }
    }

    public class FileResultsStorage
    {
        /// <summary>
        ///  Required - path to store log files
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// Optional - Filename for logs. If not provided, default value will be used
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Optional - If file exists, should it be overwritten each run? If not provided, default value will be used (false)
        /// </summary>
        public bool Overwrite { get; set; }

    }
}
