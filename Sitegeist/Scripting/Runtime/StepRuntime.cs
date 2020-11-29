using Sitegeist.Scripting.Runtime.Actions;
using System.Collections.Generic;
using Sitegeist.Scripting.Runtime.Expects;
using Sitegeist.Scripting.Runtime.Getters;

namespace Sitegeist.Scripting.Engine
{
    public class StepRuntime
    {
        public string StepID { get; set; }
        public string StepName { get; set; }        
        
        public List<LogMessageActionRuntime> BeforeLog { get; set; }
        public List<IScriptActionRuntime> Actions { get; set; }
        public List<IScriptExpectRuntime> Expectations { get; set; }
        public List<IGetterRuntime> Getters { get; set; }
        
        public List<LogMessageActionRuntime> AfterLog { get; set; }       

        public ScriptRuntime EmbeddedScript { get; set; }
        
        public bool BeforeScreenshot { get; set; }
        public bool AfterScreenshot { get; set; }
        public bool DiffScreenshot { get; set; }


        public StepRuntime()
        {
            BeforeLog = new List<LogMessageActionRuntime>();
            Getters = new List<IGetterRuntime>();
            Actions = new List<IScriptActionRuntime>();
            Expectations= new List<IScriptExpectRuntime>();
            AfterLog = new List<LogMessageActionRuntime>();
        }
    }
}
