
using Sitegeist.Scripting.Engine;
using System;
using System.IO;

namespace Sitegeist.Utils
{
    public static class FilePathBuilder
    {
        public static string BuildPath(string directory, string file, bool overwrite)
        {
            file = ValueReplacer.InjectVariables(new MemoryGlobalVariables(), file);

            var fi = new FileInfo($"{directory}\\{file}");
            var fileBase = fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length);

            if (!fi.Directory.Exists)
                fi.Directory.Create();

            if (fi.Exists)
            {
                if (!ScriptEngine.ConfigurationSettings.Settings.FileLoggerSettings.Overwrite)
                    fi = new FileInfo($"{fi.DirectoryName}\\{fileBase}_{DateTime.Now.ToString("MMddyyyyHHmmss")}{fi.Extension}");
                else
                    fi.Delete();
            }

            return fi.FullName;
        }
    }
}
