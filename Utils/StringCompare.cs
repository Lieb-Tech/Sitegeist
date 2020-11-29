using Sitegeist.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sitegeist.Utils
{
    public static class StringCompare
    {
        public static bool IsMatch(string String1, StringMatchTypes MatchType, string String2)
        {
            if (MatchType == StringMatchTypes.ExactMatch)
            {
                return String1.Trim() == String2.Trim();
            }
            else if (MatchType == StringMatchTypes.ExactMatchCaseInsensative)
            {
                return String1.ToLower().Trim() == String2.ToLower().Trim();
            }
            else if (MatchType == StringMatchTypes.StartsWith)
            {
                return String1.Trim().StartsWith(String2.Trim());
            }
            else if (MatchType == StringMatchTypes.StartsWithCaseInsensative)
            {
                return String1.ToLower().Trim().StartsWith(String2.ToLower().Trim());
            }
            else if (MatchType == StringMatchTypes.EndsWith)
            {
                return String1.Trim().EndsWith(String2.Trim());
            }
            else if (MatchType == StringMatchTypes.EndsWithCaseInsensative)
            {
                return String1.ToLower().Trim().EndsWith(String2.ToLower().Trim());
            }
            else if (MatchType == StringMatchTypes.Contains)
            {
                return String1.Trim().Contains(String2.Trim());
            }
            else if (MatchType == StringMatchTypes.ContainsCaseInsensative)
            {
                return String1.ToLower().Trim().Contains(String2.ToLower().Trim());
            }

            return false;
        }
    }
}
