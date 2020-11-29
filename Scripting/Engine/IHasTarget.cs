
using Sitegeist.Scripting.Paths;

namespace Sitegeist.Scripting.Engine
{
    interface IHasTarget
    {
        IPath Target { get; set; }
    }
}
