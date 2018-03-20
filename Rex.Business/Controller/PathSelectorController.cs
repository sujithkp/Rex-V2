using Rex.Business.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.Business.Controller
{
    public class PathSelectorController
    {
        private IPathSelector View;

        public PathSelectorController()
        {
            this.View = new PathSelectorWindow();
        }

        public string[] GetUserSelectedPath(IEnumerable<IEnumerable<string>> paths)
        {
            var selectedPath = this.View.GetSelectedPath(paths.Select(x => string.Join(" > ", x)).ToList());

            return selectedPath.Split(new string[] { " > " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
