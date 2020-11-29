
using OpenQA.Selenium;

namespace Sitegeist.Scripting.Paths
{
    public class ElementPath : IPath
    {
        public PathTypes PathType { get; set; }
        public string Path { get; set; }

        public By ToBy()
        {
            if (PathType == PathTypes.ID)
                return  By.Id(Path);
            else if (PathType == PathTypes.XPath)
                return By.XPath(Path);
            else if (PathType == PathTypes.CSS)
                return By.CssSelector(Path);
            else if (PathType == PathTypes.Name)
                return By.Name(Path);

            return null;
        }
        public override string ToString()
        {
            return $"({PathType}) {Path}";
        }
    }
}
