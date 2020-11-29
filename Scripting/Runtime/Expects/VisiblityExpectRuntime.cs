using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Expects;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Runtime.Expects
{
    public class VisiblityExpectRuntime : BaseExpectRuntime, IScriptExpectRuntime
    {
        public VisiblityExpectRuntime(VisiblityExpectConfig config) : base(config)
        {
            ShouldBeVisible = config.ShouldBeVisible;
        }

        public bool ShouldBeVisible { get; set; }

        public override Dictionary<string, string> Parameters => new Dictionary<string, string>()
        {
            { "ShouldBeVisible", ShouldBeVisible.ToString() }
        };

        public override bool CheckExpectation(IWebDriver webDriver)
        {
            var element = Utils.GetElementByPath.GetElement(Target, webDriver);

            var meetsExpectation = element.Displayed == ShouldBeVisible;
            if (!meetsExpectation)
                Message = $"`{Target.Path}` expected to be {(ShouldBeVisible ? "Visible" : "Hidden")} but was not";

            return meetsExpectation;
        }
    }
}
