using Rex.Common;
using Rex.UI.Lib;
using System;

namespace Rex.UI.Controls
{
    public class TableNode : RexNode
    {
        public TableNode(String tableText, String targetTable, KeySet key)
            : base(tableText, key)
        {
            this.Nodes.Add(".");

            this.Table = targetTable;

            if (Properties.Settings.Default.Singularize)
                if (tableText.EndsWith("s") || tableText.EndsWith("S"))
                    this.Text = tableText.Substring(0, tableText.Length - 1);

        }
    }
}
