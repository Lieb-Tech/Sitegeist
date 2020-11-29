# Sitegeist
### Proof of concept for a configuration based Selenium Web testing system.

Trying out a way to do behavioral testing of websites with Selenium. In the past I've used this excellent toolkit for code driven testing. Writing these tests could be a bit labor intensive. 

There are several Chrome plugins for "recording" workflows - these helped to identify the IDs, Names and XPaths to the elements, but still needed to write code to perform the tests.

Sitegeist is a proof of concept of having a configuration approach to executing the tests, and verifying things go as expected. These are the core concepts of this project:

### Importers 
Many of the Chrome recording plugins are able to export out the "scripts" in various formats - json, xml, even code files. An importer in Sitegeist will read in those exports and create Sitegeist Script files. Using the IImporter interface, additional importers can be created

### Script Files
These are the configuration of the actions and expectations. These contain:
- Steps: A set of actions, expectations and getters groupped together
- Actions: Actions to take on a page or against a specific element - clicking a button, entering text, logging information to the logs, etc. (IScriptActionConfig / IScriptActionRuntime interface for creating new actions)
- Expectations: Verfication that an expected behavior occured - an element is visible, the current page's url, text in an element, etc. (IScriptExpectConfig / IScriptExpectRuntime interfaces for creating new expectations)
- Getter: Extract information from the page into a variable - text in an element

### Variables 
Sometimes there are variations of the same tests, but with different values entered in a field. Other actions/expectations need values from a previous step. Sitegeist allows for placeholders in strings to be replaced with values captured elsewhere ont the page or variables defined in the script itself. 

### Variable Injection
A Getter can extract a value from an element and save to a variable, ex: labelText. Then an action later on can use that value in textbox, using the format "{labelText}".

There are several system formats as well, specifically for Datetime variations 

### Input/Ouput
- Logging: Sitegeist can write logging information to different places: File, Console, etc. The ILogger interface allows for extensibility, so that you can write to an on-prem database or one in the cloud
- LogMessageAction: This Action will write Variable values to the configured logger for later evaluation
- Scripts/Results: These can be stored on local disk, or using the appropriate interfaces, can be persisted to a database or elsewhere

### Snapshot
- ElementSnapshotAction: Take screenshot of the page and crop to the element for visual review later on
- Step: Take a snapshot before and/or afterward a step is completed to see the changes on the pages
- Diff: Diffs can be taken of a step before/after or from previous executions to see what has changed

### Extensibility
All user facing functionality is based on interfaces, so that different Actions, Expects, Getters, Loggers, Script/Result stores can easily be added to the system.

Future to dos:
- User interface 
  - make a web interface for to view/create/edit scripts 
  - view/compare results 
  - set baseline snapshots for steps
