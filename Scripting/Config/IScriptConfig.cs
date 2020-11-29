using Sitegeist.Scripting.Engine;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Config.Engine
{
    public interface IScriptConfig
    {
        bool ContinueOnFailedExpectations { get; set; }
        string ScreenshotPath { get; set; }
        string ScriptID { get; set; }
        string ScriptName { get; set; }
        string StartingUrl { get; set; }
        List<StepConfig> Steps { get; set; }
        List<Variable> Variables { get; set; }  
    }
}