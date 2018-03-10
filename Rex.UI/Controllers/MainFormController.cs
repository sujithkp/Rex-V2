using Rex.Business;
using Rex.Common.Connection;
using Rex.Common.Data;
using Rex.UI.Controls;
using Rex.UI.Lib;
using System.Collections.Generic;
using System.Linq;

namespace Rex.UI.Controllers
{
    public class MainFormController
    {
        private DataStore store;
        private bool IsConnected = false;
        private ConnectionProperties _connectionProperties;

        public MainFormController()
        {
            store = new DataStore();
        }

        public ConnectionProperties Connect()
        {
            IsConnected = (_connectionProperties = store.Connect()) != null;
            return _connectionProperties;
        }

        public TablePrimaryKeys AddRecord()
        {
            return store.GetNewRecordKeys();
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
                nodes.Add(new TableNode(table.Source.Column, table.Target.Table, pkeyForTargetTbl));
            }

            var referencingTabels = store.GetTablesReferencing(tableNode.Table);
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
            if (rows.Count() == 0)
                return null;

            var primaryCols = store.GetPrimaryColumns(tableCollection.Table);

            if (primaryCols.Count() == 0)
                primaryCols = store.GetForeignKeyColumns(tableCollection.Table);

            foreach (var row in rows)
            {
                var pkeyForTargetTbl = new PrimaryKeySet();
                foreach (var primaryCol in primaryCols)
                    pkeyForTargetTbl.PrimaryKeys.Add(new ColumnValueSet(primaryCol, row.GetValueFor(primaryCol)));

                nodes.Add(new TableNode(tableCollection.Table, tableCollection.Table, pkeyForTargetTbl));
            }

            return nodes;
        }

        public IEnumerable<Row> GetRows(TableCollectionNode tableCollection)
        {
            return store.GetRows(tableCollection.Table, tableCollection.keys);
        }

        public Row GetRows(TableNode table)
        {
            return store.GetRow(table.Table, table.keys);
        }
    }
}
