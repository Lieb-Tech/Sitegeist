using Sitegeist.Scripting.Paths;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitegeist.Scripting.Engine
{
    public class ScriptRunResults
    {
        public DateTime Started { get; set; }
        public string ScriptName { get; set; }
        public string StartingUrl { get; set; }
        public Dictionary<string, string> Variables { get; set; }

        public List<StepResult> StepResults { get; set; }
        public ScriptRunResults()
        {
            StepResults = new List<StepResult>();
            Variables = new Dictionary<string, string>();
        }
    }

    public class StepResult
    {
        public string StartingUrl { get; set; }
        public string EndingUrl { get; set; }
        public bool? MetExpectation { get => !ExpectResults.Any() ? (bool?)null : !ExpectResults.Any(z => !z.MetExpectation); }
        public Dictionary<string, string> GetVariables { get; set; }
        public List<RunnerActionResults> ActionResults { get; set; }
        public List<RunnerExpectResults> ExpectResults { get; set; }
        public StepResult()
        {
            GetVariables = new Dictionary<string, string>();
            ActionResults = new List<RunnerActionResults>();
            ExpectResults = new List<RunnerExpectResults>();            
        }
    }

    public class RunnerExpectResults
    {
        public IPath Target { get; set; }
        public bool MetExpectation { get; set; }
        public RunnerException EpectException { get; set; }
        public Dictionary<string, string> EpectArguments { get; set; }
        public string ExpectationMessage { get; set; }
        public string ExpectType { get; set; }
    }

    public class RunnerException
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }

    public class RunnerActionResults
    {
        public RunnerException ActionException { get; set; }
        public string ActionType { get; set; }
        public IPath Target { get; set; }
        public Dictionary<string,string> ActionArguments { get; set; }
    }

}