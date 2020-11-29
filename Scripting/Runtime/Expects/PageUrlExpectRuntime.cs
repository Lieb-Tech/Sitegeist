using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Expects;
using Sitegeist.Utils;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Expects
{
    /// <summary>
    /// Verify that the page is on a specific URL 
    /// Future: allow relative and pattern based URL checks
    /// </summary>
    public class PageUrlExpectRuntime : BaseExpectRuntime, IScriptExpectRuntime
    {
        public PageUrlExpectRuntime(PageUrlExpectConfig config) : base(config)
        {
            MatchType = config.MatchType;
            Url = config.Url;
        }
        public StringMatchTypes MatchType { get; set; }
        public string Url { get; set; }

        public override Dictionary<string, string> Parameters => new Dictionary<string, string>()
            {
                { "Url", Message }
            };

        public override bool CheckExpectation(IWebDriver webDriver)
        {
            var matches = StringCompare.IsMatch(webDriver.Url, MatchType, Url);
            if (!matches)
                Message = $"URL expected to be `{Url}` but was `{webDriver.Url}` (Match type: {MatchType})";

            return matches;
        }
    }
}