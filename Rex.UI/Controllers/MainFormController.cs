using Rex.Business;
using Rex.Common.Data;
using Rex.UI.Controls;
using Rex.UI.Lib;
using System.Collections.Generic;

namespace Rex.UI.Controllers
{
    public class MainFormController
    {
        private DataStore store;

        public MainFormController()
        {
            store = new DataStore();
        }

        public IList<RexNode> GetDependants(TableNode tableNode)
        {
            var nodes = new List<RexNode>();

            var dataRow = store.GetRow(tableNode.Table, tableNode.keys);

            var referencedTables = store.GetTablesReferencedBy(tableNode.Table);
            foreach (var table in referencedTables)
            {
                var pkeyForTargetTbl = new PrimaryKeySet();
                pkeyForTargetTbl.PrimaryKeys.Add(new ColumnValueSet(table.Target.Column, dataRow.GetValueFor(table.Source.Column)));
                nodes.Add(new TableNode(table.Target.Table, table.Target.Table, pkeyForTargetTbl));
            }

            var referencingTabels = store.GetTablesReferencing(tableNode.Text);
            foreach (var table in referencingTabels)
            {
                var fkeyFortargetTbl = new ForeignKeySet();
                fkeyFortargetTbl.ForeignKeys.Add(new ColumnValueSet(table.Source.Column, dataRow.GetValueFor(table.Target.Column)));
                nodes.Add(new TableCollectionNode(table.Source.Table, table.Source.Table, fkeyFortargetTbl));
            }

            return nodes;
        }

        public IList<RexNode> GetDependants(TableCollectionNode tableCollection)
        {
            var nodes = new List<RexNode>();

            var rows = store.GetRows(tableCollection.Table, tableCollection.keys);
            var primaryCols = store.GetPrimaryColumns(tableCollection.Table);

            foreach(var row in rows)
            {
                var pkeyForTargetTbl = new PrimaryKeySet();
                foreach (var primaryCol in primaryCols)
                {
                    pkeyForTargetTbl.PrimaryKeys.Add(new ColumnValueSet(primaryCol, row.GetValueFor(primaryCol)));
                    nodes.Add(new TableNode(tableCollection.Table, tableCollection.Table, pkeyForTargetTbl));
                }
            }

            return nodes;
        }
    }
}
