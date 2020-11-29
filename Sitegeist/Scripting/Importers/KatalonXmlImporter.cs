using Sitegeist.Scripting.Config.Actions;
using Sitegeist.Scripting.Config.Engine;
using Sitegeist.Scripting.Config.Expects;
using Sitegeist.Scripting.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sitegeist.Scripting.Importers
{
    public class KatalonXmlImporter : IImporter
    {        
        public ScriptConfig ImportRaw(string rawData)
        {
            var script = new ScriptConfig();
            XElement xml = XElement.Parse(rawData);
            var ele = xml.Elements("selenese")
                .FirstOrDefault(z => z.Elements().Any(a => a.Value.ToString() == "open"));

            if (ele != null)
            {
                var value = ele.Elements("target").FirstOrDefault().Value;
                script.StartingUrl = value;
                ele.Remove();
            }

            foreach (var el in xml.Elements("selenese"))
            {
                script.Steps.Add(processElement(el));
            }
            
            script.ScriptName = "Katalon Xml Imported";
            
            return script;
        }

        private StepConfig processElement(XElement el)
        {
            var action = el.Element("command").Value;
            var target = el.Element("target").Value;
            var value = el.Element("value").Value;

            IScriptActionConfig scriptAction = null;
            IPath path = getPath(target);
            switch (action)
            {                
                case "logMessage":
                    scriptAction = new LogMessageActionConfig()
                    {
                         Message = value
                    };
                    break;
                case "submit":
                    scriptAction = new FormSubmitActionConfig()
                    {
                        Target = path
                    };
                    break;
                case "click":                
                    scriptAction = new ClickActionConfig()
                    {
                        Target = path
                    };
                    break;
                case "type":
                    scriptAction = new EnterTextActionConfig()
                    {
                        Text = value,
                        Target = path
                    };
                    break;
                case "special":
                    scriptAction = new EnterTextActionConfig()
                    {
                        Text = value,
                        Target = path
                    };
                    break;                
            }

            IScriptExpectConfig scriptExpects = null;
            switch (action.ToLower())
            {
                case "doubleclick":
                    scriptExpects = parseExpects(target, value);
                    break;
                case "verifytext":
                case "elementtext":
                    scriptExpects = new ElementTextExpectConfig()
                    {
                        MatchType = StringMatchTypes.ContainsCaseInsensative,
                        Target = path,
                        Text = value
                    };                
                    break;
            }

            return new StepConfig()
            {
                
                Expectations = new List<IScriptExpectConfig>()
                {
                    scriptExpects
                },
                Actions = new List<IScriptActionConfig>()
                {
                    scriptAction
                }
            };
        }

        private IScriptExpectConfig parseExpects(string target, string value)
        {
            if (value.ToLower() == "visible")
            {
                return new VisiblityExpectConfig()
                {
                    Target = getPath(target),
                    ShouldBeVisible = true
                };
            }        

            return null;
        }

        private IPath getPath(string target)
        {
            if (target.StartsWith("//"))
            {
                return new ElementPath() { Path = target, PathType = PathTypes.XPath };
            }
            else
            {
                var parts = target.Split( new char[] { '=' });
                if (parts.Count() == 2)
                {
                    if (parts[0] == "name")
                        return new ElementPath() { Path = parts[1], PathType = PathTypes.Name };
                    else if (parts[0] == "id")
                        return new ElementPath() { Path = parts[1], PathType = PathTypes.ID };                    
                }
            }
            return null;
        }

        public ScriptConfig ImportScript(string path)
        {
            throw new NotImplementedException();
        }
    }
}
