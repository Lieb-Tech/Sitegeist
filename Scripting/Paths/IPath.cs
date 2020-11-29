
using OpenQA.Selenium;

namespace Sitegeist.Scripting.Paths
{
    public interface IPath
    {
        string ToString();
        PathTypes PathType { get; set; }
        string Path { get; set; }
        By ToBy();
    }
}
