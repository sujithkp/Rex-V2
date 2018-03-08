using Rex.Common;
using Rex.UI.Lib;
using System;

namespace Rex.UI.Controls
{
    public class TableNode : RexNode
    {
        public TableNode(String column, String targetTable, KeySet key)
            : base(column, key)
        {
            this.Nodes.Add(".");

            this.Table = targetTable;
        }
    }
}
