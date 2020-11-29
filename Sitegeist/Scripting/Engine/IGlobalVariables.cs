
using System.Collections.Generic;

namespace Sitegeist.Scripting.Engine
{
    public interface IGlobalVariables
    {
        bool Any { get; }

        string Get(string Key);

        bool ContainsKey(string Key);

        void Set(string Key, string Value);

        IEnumerable<KeyValuePair<string,string>> AsEnumerable();
    }
}
