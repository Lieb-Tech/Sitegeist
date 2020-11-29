using System;
using System.Text.RegularExpressions;

namespace Sitegeist.Scripting.Engine
{
    public static class ValueReplacer
    {
        static string matcher = "{[a-zA-Z0-9:_-]+}";
        public static string InjectVariables(IGlobalVariables GlobalVariables, string Value)
        {
            var matches = Regex.Matches(Value, matcher);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {                    
                    if (GlobalVariables.ContainsKey(match.Value))
                        Value = Regex.Replace(Value, matcher, GlobalVariables.Get(match.Value));
                    else
                        Value = ReplaceValue(Value, match.Value);
                }
                return Value;
            }
            else
                return Value;            
        }

        public static string ReplaceValue(string Value, string Match)
        {
            if (Match == "{DATE}")
                Value = Value.Replace(Match, DateTime.Now.ToString("MM/dd/yyyy"));
            else if (Match == "{EPOCH}")
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0);
                Value = Value.Replace(Match, (DateTime.Now - epoch).ToString());
            }
            else if (Match == "{EPOCH_SECONDS}")
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0);                
                Value = Value.Replace(Match, ((DateTime.Now - epoch).TotalSeconds).ToString("0"));
            }
            else if (Match == "{EPOCH_MILLISECONDS}")
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0);
                Value = Value.Replace(Match, ((DateTime.Now - epoch).TotalMilliseconds).ToString("0"));
            }
            else if (Match.Contains("{EPOCH:"))
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0);
                var seperator = Match.IndexOf(":");
                var fmt = Match.Substring(seperator + 1, Match.Length - seperator - 2);
                Value = Value.Replace(Match, (DateTime.Now - epoch).ToString(fmt));
            }            
            else if (Match == "{TIME}")
                Value = Value.Replace(Match, DateTime.Now.ToString("HH:mm:ss:fff"));
            else if (Match == "{DATETIME}")
                Value = Value.Replace(Match, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff"));
            else if (Match.StartsWith("{DATETIME:"))
            {
                var seperator = Match.IndexOf(":");
                var fmt = Match.Substring(seperator + 1, Match.Length - seperator - 2);
                Value = Value.Replace(Match, DateTime.Now.ToString(fmt));
            }
            return Value;
        }

        public static string InjectValues(string Value)
        {
            var matches = Regex.Matches(Value, matcher);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    Value = ReplaceValue(Value, match.Value);
                }
                return Value;
            }
            else
                return Value;

        }
    }
}
