using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Expects;
using Sitegeist.Utils;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Expects
{
    public class ElementTextExpectRuntime : BaseExpectRuntime, IScriptExpectRuntime
    {
        public ElementTextExpectRuntime(ElementTextExpectConfig config) : base (config)
        {
            MatchType = config.MatchType;
            Text = config.Text;
        }

        public StringMatchTypes MatchType { get; set; }
        public string Text { get; set; }
        public override Dictionary<string, string> Parameters => new Dictionary<string, string>()
            {
                { "Text", Text}
            };
                
        public override bool CheckExpectation(IWebDriver webDriver)
        {
            IWebElement element = Utils.GetElementByPath.GetElement(Target, webDriver);
            var text = element.Text;

            var matches = StringCompare.IsMatch(text, MatchType, Text);

            if (!matches)
                Message = $"`{Target.Path}` expected to be `{Text}` but was `{text}` (Match type: {MatchType})";

            return matches;
        }
    }
}
