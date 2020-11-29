using Sitegeist.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Sitegeist.Scripting.Engine
{
    /// <summary>
    /// Memory shared variable store
    /// </summary>
    public class MemoryGlobalVariables : IGlobalVariables
    {
        /// <summary>
        /// Backing store
        /// </summary>
        private readonly Dictionary<string, string> variables = new Dictionary<string, string>();

        /// <summary>
        /// Does variable store have any entries
        /// </summary>
        public bool Any { get => variables.Any(); }
         
        /// <summary>
        /// Is the variable name saved yet
        /// </summary>
        /// <param name="Key">Variable name to check on</param>
        /// <returns></returns>
        public bool ContainsKey(string Key)
        {
            Key = VariableName.FormatName(Key);
            return variables.ContainsKey(Key);
        }

        public string Get(string Key)
        {
            Key = VariableName.FormatName(Key);
            return variables[Key];
        }

        public IEnumerable<KeyValuePair<string, string>> AsEnumerable()
        {
            return variables;
        }

        public void Set(string Key, string Value)
        {
            Key = VariableName.FormatName(Key);

            if (variables.ContainsKey(Key))
                variables[Key] = Value;
            else
                variables.Add(Key, Value);
        }
    }
}
