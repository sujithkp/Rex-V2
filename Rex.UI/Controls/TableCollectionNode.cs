using Rex.Common;
using Rex.Common.Data;
using Rex.UI.Lib;

namespace Rex.UI.Controls
{
    public class TableCollectionNode : RexNode
    {
        public TableCollectionNode(string text, string table, KeySet keys)
            : base(text, keys)
        {
            this.Text = text + "(s)";
            this.Table = table;
            this.Nodes.Add(".");
        }

        public override string ToString()
        {
            var primaryKeyset = this.keys as ForeignKeySet;
            var primaryKeyString = BuildSelectionCriteria(primaryKeyset.ForeignKeys);
            return "Forieng Key : " + this.Table + "." + primaryKeyString;
        }
    }
}
