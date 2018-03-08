using Rex.Common;
using Rex.Common.Data;
using Rex.UI.Lib;
using System.Linq;

namespace Rex.UI.Controls
{
    public class TableNode : RexNode
    {
        public TableNode(string tableText, string targetTable, KeySet key)
            : base(tableText, key)
        {
            this.Nodes.Add(".");

            this.Table = targetTable;

            if (Properties.Settings.Default.Singularize)
                if (tableText.EndsWith("s") || tableText.EndsWith("S"))
                    this.Text = tableText.Substring(0, tableText.Length - 1);
        }

        public override string ToString()
        {
            var primaryKeyset = this.keys as PrimaryKeySet;
            var primaryKeyString = BuildSelectionCriteria(primaryKeyset.PrimaryKeys);
            return "Primary Key : " + this.Table + "." + primaryKeyString;
        }
    }
}
