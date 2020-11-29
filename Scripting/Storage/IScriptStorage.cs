using Sitegeist.Scripting.Config.Engine;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Storage
{
    public interface IScriptStorage
    {
        string SaveScript(ScriptConfig script);

        ScriptConfig GetScript(string scriptID);
        List<(string, string)> GetAllScripts();

    }  
}
