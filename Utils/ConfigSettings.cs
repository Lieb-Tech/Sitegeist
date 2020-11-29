using Newtonsoft.Json;
using System.IO;

namespace Sitegeist.Utils
{
    public static class ConfigSettings
    {
        public static Blob GetBlob()
        {
            var str = File.ReadAllText("keys.json");
            var keys = JsonConvert.DeserializeObject<Key>(str);
            return keys.blob;
        }
    }

    public class Key
    {
        public Blob blob { get; set; }
    }
    public class Blob
    {
        public string key { get; set; }
        public string connection { get; set; }
    }
}