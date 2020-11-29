using Sitegeist.Scripting.Engine;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Config.Engine
{
    public class ScriptConfig : IScriptConfig
    {
        public string ScriptID { get; set; }
        public string ScreenshotPath { get; set; }
        public List<Variable> Variables { get; set; }
        public string StartingUrl { get; set; }
        public bool ContinueOnFailedExpectations { get; set; }
        public string ScriptName { get; set; }
        public List<StepConfig> Steps { get; set; }
        public ScriptConfig()
        {
            ScreenshotPath = @"c:\temp\";
            ContinueOnFailedExpectations = false;
            Steps = new List<StepConfig>();
            Variables = new List<Variable>();
        }
    }
}
