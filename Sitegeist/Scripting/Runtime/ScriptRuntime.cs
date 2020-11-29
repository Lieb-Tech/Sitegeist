using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Config.Engine;
using Sitegeist.Scripting.Config.Expects;
using Sitegeist.Scripting.Config.Getters;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Runtime.Actions;
using Sitegeist.Scripting.Runtime.Expects;
using Sitegeist.Scripting.Runtime.Getters;
using System.Collections.Generic;

namespace Sitegeist.Scripting.Engine
{
    public class ScriptRuntime 
    {        
        public string ScriptID { get; set; }
        public ILogger Logger { get; set; }
        public string ScreenshotPath { get; set; }
        public IGlobalVariables Variables { get; set; }
        public string StartingUrl { get; set; }
        public bool ContinueOnFailedExpectations { get; set; }
        public string ScriptName { get; set; }
        public List<StepRuntime> Steps { get; set; }        

        public ScriptRuntime()
        {
            Logger = new NullLogger();
            ScreenshotPath = @"c:\temp\";
            ContinueOnFailedExpectations = false;
            Steps = new List<StepRuntime>();
            Variables = new MemoryGlobalVariables();   
        }

        // take a config and convert to runtime
        public ScriptRuntime(IScriptConfig scriptConfig, IGlobalVariables variables)
        {
            // copy basic infos
            ScriptID = scriptConfig.ScriptID;
            ScreenshotPath = scriptConfig.ScreenshotPath;
            StartingUrl = scriptConfig.StartingUrl;
            ScriptName = scriptConfig.ScriptName;

            Steps = new List<StepRuntime>();
            Variables = variables;
            foreach (var variable in scriptConfig.Variables)
            {
                Variables.Set(variable.Name, variable.Value);
            }

            // convert each step from config to runtime 
            foreach (var step in scriptConfig.Steps)
            {
                var newStep = new StepRuntime();

                if (step.EmbeddedScript != null)
                    newStep.EmbeddedScript = new ScriptRuntime(step.EmbeddedScript, Variables);
                
                configureActions(step, newStep);
                configureExpectations(step, newStep);
                configureGetters(step, newStep);

                configureLoggers(step, newStep);

                Steps.Add(newStep);
            }
        }

        internal void configureLoggers(StepConfig step, StepRuntime newStep)
        {
            foreach (var logger in step.BeforeLog)
            {
                switch (logger)
                {
                    case LogMessageActionConfig config:
                        newStep.BeforeLog.Add(new LogMessageActionRuntime(config));
                        break;
                }
            }
            foreach (var logger in step.AfterLog)
            {
                switch (logger)
                {
                    case LogMessageActionConfig config:
                        newStep.AfterLog.Add(new LogMessageActionRuntime(config));
                        break;
                }
            }
        }
        internal void configureGetters(StepConfig step, StepRuntime newStep)
        {
            foreach (var getter in step.Getters)
            {
                switch (getter)
                {
                    case TextGetterConfig config:
                        newStep.Getters.Add(new TextGetterRuntime(config));
                        break;
                }
            }
        }

        internal void configureExpectations(StepConfig step, StepRuntime newStep)
        {
            foreach (var exp in step.Expectations)
            {
                switch (exp)
                {
                    case ElementTextExpectConfig config:
                        newStep.Expectations.Add(new ElementTextExpectRuntime(config));
                        break;
                    case PageUrlExpectConfig config:
                        newStep.Expectations.Add(new PageUrlExpectRuntime(config));
                        break;
                    case VisiblityExpectConfig config:
                        newStep.Expectations.Add(new VisiblityExpectRuntime(config));
                        break;
                }
            }
        }

        internal void configureActions(StepConfig step, StepRuntime newStep)
        {
            foreach (var act in step.Actions)
            {
                switch (act)
                {
                    case ClickActionConfig config:
                        newStep.Actions.Add(new ClickActionRuntime(config));
                        break;
                    case EnterTextActionConfig config:
                        newStep.Actions.Add(new EnterTextActionRuntime(config));
                        break;
                    case FormSubmitActionConfig config:
                        newStep.Actions.Add(new FormSubmitActionRuntime(config));
                        break;
                    case LogMessageActionConfig config:
                        newStep.Actions.Add(new LogMessageActionRuntime(config));
                        break;
                    case SpecialKeyActionConfig config:
                        newStep.Actions.Add(new SpecialKeyActionRuntime(config));
                        break;
                    case WaitForVisibilityActionConfig config:
                        newStep.Actions.Add(new WaitForVisibilityActionRuntime(config));
                        break;
                    case MultipleTextActionConfig config:
                        newStep.Actions.Add(new MultipleTextActionRuntime(config));
                        break;
                }
            }
        }
    }
}
