using Rex.Common;
using Rex.Common.Data;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rex.UI.Lib
{
    public abstract class RexNode : TreeNode
    {
        public RexNode(string text, KeySet keys)
            : base(text)
        {
            this.keys = keys;
        }

        public KeySet keys { get; private set; }

        public string Table { get; protected set; }

        protected string BuildSelectionCriteria (IList<ColumnValueSet> columns)
        {
            return string.Join(",", columns.Select(x => x.Name + "=" + "'" + x.Value + "'"));
        }
    }
}
