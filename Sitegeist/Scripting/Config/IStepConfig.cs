using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Config.Expects;
using Sitegeist.Scripting.Config.Getters;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Config.Engine
{
    public interface IStepConfig
    {
        List<IScriptActionConfig> Actions { get; set; }
        List<LogMessageActionConfig> AfterLog { get; set; }
        bool AfterScreenshot { get; set; }
        List<LogMessageActionConfig> BeforeLog { get; set; }
        bool BeforeScreenshot { get; set; }
        bool DiffScreenshot { get; set; }
        List<IScriptExpectConfig> Expectations { get; set; }
        List<IGetterConfig> Getters { get; set; }
        ScriptConfig EmbeddedScript { get; set; }
        string StepID { get; set; }
        string StepName { get; set; }
    }
}