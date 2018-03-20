using System.Collections.Generic;

namespace Rex.Business.UI
{
    public interface IPathSelector
    {
        string GetSelectedPath(IList<string> paths);
    }
}
