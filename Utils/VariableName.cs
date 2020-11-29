using System;

namespace Sitegeist.Utils
{
    public static class VariableName
    {
        public static string FormatName(string variableName)
        {
            if (!variableName.StartsWith("{") && !variableName.EndsWith("}"))
                variableName = "{" + variableName + "}";

            return variableName;
        }
    }
}
