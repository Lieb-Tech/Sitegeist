using OpenQA.Selenium.Chrome;
using Sitegeist.Scripting.Config.Getters;
using Sitegeist.Scripting.Importers;
using Sitegeist.Scripting.Loggers;
using Sitegeist.Utils;

namespace Sitegeist.Scripting.Engine
{
    public class ScriptEngine
    {
		public static ConfigurationSettings ConfigurationSettings { get; private set; }
        public ScriptEngine()
        {
			ConfigurationSettings = new ConfigurationSettings();
		}

		public ScriptRunResults RunScripts()
		{
			IImporter importer = new KatalonXmlImporter();
			var imported1 = importer.ImportRaw(duckDuck);			

			var variables = new MemoryGlobalVariables();
			ChromeOptions option = new ChromeOptions();
			var webDriver = new ChromeDriver(option);

			var logger = new FileLogger();
			var runner = new ScriptRunner(webDriver, imported1, variables, logger);
			
			var results1 = runner.RunScript();

			webDriver.Close();
			webDriver.Dispose();

			SaveResults(results1);						
			
			return results1;
        }

		internal void SaveResults(ScriptRunResults results)
        {
			if (ConfigurationSettings.Settings.FileResultsStorage != null)
            {
				var fileResults = new Storage.FileResultsStorage();
				
				fileResults.SaveResults(results);
			}
		}


		#region xmlStrings 
		static string duckDuck = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<TestCase>
<selenese>
	<command>open</command>
	<target><![CDATA[https://duckduckgo.com/]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=search_form_input_homepage]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>type</command>
	<target><![CDATA[id=search_form_input_homepage]]></target>
	<value><![CDATA[Search here]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=search_button_homepage]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>verifyText</command>
	<target><![CDATA[//div[@data-domain][1]/div/div[2]]]></target>
	<value><![CDATA[Search the world's information, including webpages, images, videos and more. Google has many special features to help you find exactly what you're looking for.]]></value>
</selenese>
</TestCase>";


        static string xmlAccord = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<TestCase>
<selenese>
	<command>open</command>
	<target><![CDATA[https://jqueryui.com/resources/demos/accordion/default.html]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-2']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-2']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>doubleClick</command>
	<target><![CDATA[//div[@id='ui-id-2']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-3]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-4']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-5]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-6']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-7]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-8]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-8]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-8']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>verifyText</command>
	<target><![CDATA[//div[@id='ui-id-8']/p]]></target>
	<value><![CDATA[Cras dictum.Pellentesque habitant morbi tristique senectus et netus

        et malesuada fames ac turpis egestas.Vestibulum ante ipsum primis in

        faucibus orci luctus et ultrices posuere cubilia Curae; Aenean lacinia

        mauris vel est.]]></value>
</selenese>
</TestCase>";

		static string xmlAccord2 = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<TestCase>
<selenese>
	<command>open</command>
	<target><![CDATA[https://jqueryui.com/resources/demos/accordion/default.html]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-1]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-2']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-2']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>doubleClick</command>
	<target><![CDATA[//div[@id='ui-id-2']/p]]></target>
	<value><![CDATA[Visible]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-3]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-4]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-4]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>doubleClick</command>
	<target><![CDATA[id=ui-id-4]]></target>
	<value><![CDATA[Visible]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-5]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-6']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-6']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>doubleClick</command>
	<target><![CDATA[//div[@id='ui-id-6']/p]]></target>
	<value><![CDATA[Visible]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=accordion]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[id=ui-id-7]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-8']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[//div[@id='ui-id-8']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>doubleClick</command>
	<target><![CDATA[//div[@id='ui-id-8']/p]]></target>
	<value><![CDATA[]]></value>
</selenese>
</TestCase>";

		static string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<TestCase>
<selenese>
	<command>open</command>
	<target><![CDATA[https://www.google.com/]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>click</command>
	<target><![CDATA[name=q]]></target>
	<value><![CDATA[]]></value>
</selenese>
<selenese>
	<command>type</command>
	<target><![CDATA[name=q]]></target>
	<value><![CDATA[test]]></value>
</selenese>
<selenese>
	<command>submit</command>
	<target><![CDATA[id=tsf]]></target>
	<value><![CDATA[]]></value>
</selenese>
</TestCase>";
        #endregion
    }
}
