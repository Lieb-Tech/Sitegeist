using Newtonsoft.Json;
using Sitegeist.Scripting.Config.Engine;

namespace Sitegeist.Scripting.Engine
{
    public class ScriptManagement
    {
        public string SaveScript(ScriptConfig script)
        {
            var json = JsonConvert.SerializeObject(script);
            // would save to DB at this point
            return System.Guid.NewGuid().ToString();
        }

        public ScriptConfig GetScript(string scriptId)
        {
            var script = JsonConvert.DeserializeObject<ScriptConfig>(scriptId);
            return script;
        }
    }
}
