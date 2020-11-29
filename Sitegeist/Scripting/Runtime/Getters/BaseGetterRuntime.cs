using OpenQA.Selenium;
using Sitegeist.Scripting.Config.Getters;
using Sitegeist.Scripting.Engine;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Runtime.Getters
{
    public class BaseGetterRuntime : IGetterRuntime, IGetterConfig
    {
        public BaseGetterRuntime(IGetterConfig config)
        {
            // if config has Target path, then apply here as well
            if (config is IHasTarget targetConfig)
                Target = targetConfig.Target;

            VariableName = config.VariableName;
        }

        public ILogger Logger { get; set; }
        public IPath Target { get; set; }
        public string VariableName { get; set; }

        public virtual string GetValue(IWebDriver webDriver)
        {
            throw new System.NotImplementedException();
        }
    }
}
