using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Config.Expects;
using Sitegeist.Scripting.Config.Getters;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Config.Engine
{
    public class StepConfig : IStepConfig
    {
        public string StepID { get; set; }
        public string StepName { get; set; }


        public List<LogMessageActionConfig> BeforeLog { get; set; }
        public List<IScriptActionConfig> Actions { get; set; }
        public List<IScriptExpectConfig> Expectations { get; set; }
        public List<IGetterConfig> Getters { get; set; }

        public List<LogMessageActionConfig> AfterLog { get; set; }

        public ScriptConfig EmbeddedScript { get; set; }

        public bool BeforeScreenshot { get; set; }
        public bool AfterScreenshot { get; set; }
        public bool DiffScreenshot { get; set; }


        public StepConfig()
        {
            BeforeLog = new List<LogMessageActionConfig>();
            Getters = new List<IGetterConfig>();
            Actions = new List<IScriptActionConfig>();
            Expectations = new List<IScriptExpectConfig>();
            AfterLog = new List<LogMessageActionConfig>();
        }
    }
}
