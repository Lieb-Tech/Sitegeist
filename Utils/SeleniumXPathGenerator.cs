
using OpenQA.Selenium;

namespace Sitegeist.Utils
{
    public static class SeleniumXPathGenerator
    {
        internal static string GenerateXPath(IWebElement childElement, string current)
        {
            string childTag = childElement.TagName;
            if (childTag.Equals("html"))
            {
                return "/html[1]" + current;
            }
            IWebElement parentElement = childElement.FindElement(By.XPath(".."));
            var childrenElements = parentElement.FindElements(By.XPath("*"));
            int count = 0;
            for (int i = 0; i < childrenElements.Count; i++)
            {
                IWebElement childrenElement = childrenElements[i];
                string childrenElementTag = childrenElement.TagName;
                if (childTag.Equals(childrenElementTag))
                {
                    count++;
                }
                if (childElement.Equals(childrenElement))
                {
                    return GenerateXPath(parentElement, "/" + childTag + "[" + count + "]" + current);
                }
            }
            return null;
        }
    }
}
