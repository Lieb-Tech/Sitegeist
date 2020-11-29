using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Sitegeist.Scripting.Config.Engine;
using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Runtime.Actions;
using Sitegeist.Scripting.Runtime.Expects;
using Sitegeist.Scripting.Runtime.Getters;
using Sitegeist.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitegeist.Scripting.Config.Getters
{
    public class ScriptRunner
    {
        // selenium webdriver
        public IWebDriver WebDriver {get; private set;}
        // current script
        public ScriptRuntime Script { get; private set; }
        // variable store
        public IGlobalVariables Variables { get; private set; }
        public ScriptRunResults Results { get; private set; }

        public ScriptRunner(IWebDriver webDriver, IScriptConfig script, IGlobalVariables variables, ILogger logger)
        {
            // save to module variables
            WebDriver = webDriver;
            Script = new ScriptRuntime(script, variables)
            {
                Logger = logger
            };

            Variables = variables;

            // setup results
            Results = new ScriptRunResults();
            Results.Started = DateTime.Now;
            Results.ScriptName = script.ScriptName;

            // message types to log
            setDefaultLoggerMessageTypes(Script.Logger);

            // start logging
            Script.Logger.Log($"Log initialized: {DateTime.Now}", MessageTypes.General);
            Script.Logger.Log($"Starting script named: {Script.ScriptName} ({Script.ScriptID})", MessageTypes.General);
        }

        public ScriptRunner(IWebDriver webDriver, ScriptRuntime script, IGlobalVariables variables)
        {
            // save to module variables
            WebDriver = webDriver;
            Script = script;
            Variables = variables;

            // setup results
            Results = new ScriptRunResults();
            Results.Started = DateTime.Now;
            Results.ScriptName = script.ScriptName;
        }

        public ScriptRunResults RunScript()
        {            
            // navigate to starting page 
            setUrl(WebDriver, Script.StartingUrl);

            Results.StartingUrl = Script.StartingUrl;

            // ready to start start processing the steps
            foreach (var step in Script.Steps)
            {
                // if this step is a reference, the process that nested script
                if (step.EmbeddedScript != null)
                {
                    // run the other script now
                    var rs = new ScriptRunner(WebDriver, step.EmbeddedScript, Variables);
                    step.EmbeddedScript.Logger = Script.Logger;
                    var scriptResult = rs.RunScript();

                    // merge results into current set
                    foreach (var sr in scriptResult.StepResults)
                    {
                        Results.StepResults.Add(sr);
                    }

                    // merge variables into result 
                    foreach (var v in scriptResult.Variables)
                    {
                        Results.Variables.Add(v.Key, v.Value);
                    }
                }
                else
                {
                    // run this step
                    var result = processStep(step);
                    Results.StepResults.Add(result);

                    // save to results
                    foreach (var v in Variables.AsEnumerable())
                    {
                        if (Results.Variables.ContainsKey(v.Key))
                            Results.Variables[v.Key] = v.Value;
                        else
                            Results.Variables.Add(v.Key, v.Value);
                    }

                    // if an expectation failed, then optionally don't continue running
                    if (Script.ContinueOnFailedExpectations && (result.MetExpectation.HasValue && !result.MetExpectation.Value))
                        break;
                }
            }

            return Results;
        }

        // Run the step
        internal StepResult processStep(StepRuntime step)
        {
            // setup
            var stepResult = new StepResult();
            Screenshot before = null, after = null;

            stepResult.StartingUrl = WebDriver.Url;

            // ensure it has a step
            if (string.IsNullOrEmpty(step.StepID))
                step.StepID = Guid.NewGuid().ToString();
            
            Script.Logger.Log($"******* Step: {step.StepName} ({step.StepID})***********", MessageTypes.General);            

            // take screenshot before actions taken
            if (step.BeforeScreenshot)
                before = takeSnapshot(step.StepID + "_before.png", "Before");

            // user "before" logging
            userLogging(step.BeforeLog);

            // do the actions
            stepResult.ActionResults.AddRange(executeActions(step.Actions));

            // check if it did what you expected
            stepResult.ExpectResults.AddRange(checkExpectations(step.Expectations));
            
            // save value from webpage
            getValues(step.Getters);

            // user "after" logging
            userLogging(step.AfterLog);

            // take a snapshot after the actions
            if (step.AfterScreenshot)
                after = takeSnapshot(step.StepID + "_after.png", "After");

            // compare before/after screenshots
            if (step.DiffScreenshot)
                snapshotDiff(before, after, step.StepID);

            stepResult.EndingUrl = WebDriver.Url;

            return stepResult;
        }

        // if provided, set starting page Url, otherwise just log current
        internal void setUrl(IWebDriver WebDriver, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Script.Logger.Log($"No starting page = using current URL: {WebDriver.Url}", MessageTypes.General);
            }
            else
            {
                Script.Logger.Log($"Navigating to: {url}", MessageTypes.General);
                WebDriver.Url = url;
            }
        }

        // default to all types, expect Internal
        internal void setDefaultLoggerMessageTypes(ILogger logger)
        {
            MessageTypes msgType = MessageTypes.General;            
            foreach (var type in Enum.GetValues(typeof(MessageTypes)).Cast<MessageTypes>()) //.Where(z => z != MessageTypes.Internal))
            {
                msgType |= type;
            }
            
            Script.Logger.SetMessageType(msgType);
        }

        // compare before/after screenshots and output image highlighting difference
        internal void snapshotDiff(Screenshot before, Screenshot after, string fileBase)
        {
            if (before != null && after != null)
            {
                var path = $"{Script.ScreenshotPath}{fileBase}_Diff.png";
                Script.Logger.Log("Performing snapshot diff", MessageTypes.Internal);
                var diff = SnapshotCompare.Difference(before, after);
                Script.Logger.Log($"Saving snapshot diff to: {path}", MessageTypes.General);
                diff.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        // before/after logging steps
        internal void userLogging(List<LogMessageActionRuntime> logActions)
        {
            if (logActions != null)
            {
                foreach (var logAction in logActions)
                {
                    logAction.Logger = Script.Logger;
                    if (Variables.Any)
                        logAction.InjectVariables(Variables);

                    if (Script.Logger != null)
                        Script.Logger.Log(logAction.Message, MessageTypes.UserMessage);
                }
            }
        }
       
        // Extract values from the webpage
        internal  void getValues(List<IGetterRuntime> getters)
        {
            foreach (var getter in getters)
            {
                try
                {
                    getter.Logger = Script.Logger;
                    var val = getter.GetValue(WebDriver);

                    Script.Logger.Log($"Setting global variable {getter.VariableName}", MessageTypes.General);
                    Variables.Set("{" + getter.VariableName + "}", val);
                }
                catch (Exception e)
                {
                    Script.Logger.Log("\r\n*****************\r\nERROR: "
                            + getter.ToString()
                            + "\r\n"
                            + getter.Target
                            + "\r\n"
                            + e.Message + "\r\n*****************\r\n", MessageTypes.ScriptExpect | MessageTypes.General);                    
                }
            }            
        }

        // Run the Expects in the step
        internal List<RunnerExpectResults> checkExpectations(List<IScriptExpectRuntime> expectations)
        {
            var results = new List<RunnerExpectResults>();
            bool metExpectation = false;
            foreach (var expectation in expectations)
            {
                if (expectation != null)
                {
                    var result = new RunnerExpectResults()
                    {
                        ExpectType = expectation.ToString(),
                        Target = expectation.Target,
                        ExpectationMessage = expectation.Message,
                        EpectArguments = expectation.Parameters,
                        MetExpectation = false
                    };

                    try
                    {
                        if (string.IsNullOrEmpty(expectation.ScriptExpectID))
                            expectation.ScriptExpectID = Guid.NewGuid().ToString();

                        Script.Logger.Log("Expecting: " + expectation.ToString(), MessageTypes.ScriptExpect | MessageTypes.General);
                        result.MetExpectation = expectation.CheckExpectation(WebDriver);
                        if (!result.MetExpectation)
                        {
                            Script.Logger.Log("FAILED", MessageTypes.ScriptExpect | MessageTypes.General);
                            Script.Logger.Log(expectation.Message, MessageTypes.ScriptExpect | MessageTypes.General);

                            result.ExpectationMessage = expectation.Message;
                            results.Add(result);
                        } 
                        else
                        {
                            results.Add(result);
                        }
                    }
                    catch (Exception e)
                    {
                        Script.Logger.Log("\r\n*****************\r\nERROR: " 
                            + expectation.ToString()
                            + "\r\n"
                            + expectation.Target
                            + "\r\n" 
                            + e.Message + "\r\n*****************\r\n", MessageTypes.ScriptExpect | MessageTypes.General);
                        result.MetExpectation = false;
                        result.EpectException = new RunnerException()
                        {
                            Message = e.Message,
                            StackTrace = e.StackTrace
                        };
                        results.Add(result);
                    }

                    if (Script.ContinueOnFailedExpectations && !result.MetExpectation)
                        break;
                }
            }

            if (expectations.Any(z => z != null))
                Script.Logger.Log("Expectations met: " + metExpectation, MessageTypes.ScriptExpect | MessageTypes.General);
            
            return results;
        }

        // Execute the actions on the webpage
        internal List<RunnerActionResults> executeActions(List<IScriptActionRuntime> actions)
        {
            var results = new List<RunnerActionResults>();
            foreach (var action in actions)
            {
                if (action != null)
                {
                    var result = new RunnerActionResults()
                    {                        
                        ActionArguments = action.Parameters,
                        ActionType = action.ToString(),                     
                    };

                    if (action is IHasTarget targetAction)
                        result.Target = targetAction.Target;

                    try
                    {
                        if (string.IsNullOrEmpty(action.ScriptActionID))
                            action.ScriptActionID = Guid.NewGuid().ToString();

                        Script.Logger.Log("Action: " + action.ToString(), MessageTypes.ScriptAction | MessageTypes.General);
                        action.Logger = Script.Logger;

                        if (action is IInjectable && Variables.Any)
                            ((IInjectable)action).InjectVariables(Variables);

                        action.PeformAction(WebDriver);
                        results.Add(result);
                    }
                    catch(Exception e)
                    {
                        var actionTarget = (IHasTarget)action;

                        Script.Logger.Log($"\r\n*****************\r\nERROR: {action}\r\n"
                        + (actionTarget != null ? "Target: " + actionTarget.Target : "")
                        + $"\r\n{e.Message}\r\n*****************\r\n", MessageTypes.ScriptAction | MessageTypes.General);

                        result.ActionException = new RunnerException()
                        {
                            Message = e.Message,
                            StackTrace = e.StackTrace
                        };
                        results.Add(result);
                    }
                }
            }

            return results;
        }

        // take and save a screenshot of the page
        internal Screenshot takeSnapshot(string fileName, string type)
        {
            Screenshot screenshot = null;
            if (WebDriver is ChromeDriver driver)
            {
                var path = $"{Script.ScreenshotPath}_{fileName}.png";
                Script.Logger.Log($">> Taking {type} screenshot", MessageTypes.General);
                screenshot = driver.GetScreenshot();
                Script.Logger.Log($">> Saving {type} to {path}", MessageTypes.General);
                screenshot.SaveAsFile(path);
            }
            else
                Script.Logger.Log($">> Unable to take {type} screenshot - not using ChromeWebDriver", MessageTypes.General);

            return screenshot;
        }
    }
}
