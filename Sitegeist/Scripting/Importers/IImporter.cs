using Sitegeist.Scripting.Config.Engine;

namespace Sitegeist.Scripting.Importers
{
    public interface IImporter
    {
        ScriptConfig ImportScript(string path);
        ScriptConfig ImportRaw(string rawData);
    }
}