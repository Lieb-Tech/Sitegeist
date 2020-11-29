using OpenQA.Selenium;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Runtime.Getters
{
    public interface IGetterRuntime
    {
        string GetValue(IWebDriver webDriver);

        ILogger Logger { get; set; }
        IPath Target { get; set; }
        string VariableName { get; set; }
    }
}
