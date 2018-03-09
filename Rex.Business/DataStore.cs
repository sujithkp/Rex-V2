using Rex.Business.Store;
using Rex.Business.UI;
using Rex.Common;
using Rex.Common.Data;
using System.Collections.Generic;

namespace Rex.Business
{
    public class DataStore
    {
        private IDbAdapter _dataAdapter;
        private InformationSchema _schema;

        public DataStore()
        {
            _schema = new InformationSchema();
        }

        void Initialize()
        {
            _schema.Initialize(_dataAdapter.GetReferentialConstraints());
        }

        public bool Connect()
        {
            _dataAdapter = new SqlServer.Adapter.SqlServerAdapter();

            if (!_dataAdapter.Connect())
                return false;

            Initialize();
            return true;
        }

        public void AddRecord()
        {
            new Controller.NewRecordFormController().GetRecordPrimaryKeySet();
        }

        public IList<TableColumnPair> GetTablesReferencedBy(string tableName)
        {
            return _schema.GetReferencedTables(tableName);
        }

        public IList<TableColumnPair> GetTablesReferencing(string tableName)
        {
            return _schema.GetReferencingTables(tableName);
        }

        public Row GetRow(string tableName, KeySet primaryKey)
        {
            return _dataAdapter.GetRow(tableName, primaryKey);
        }

        public IEnumerable<Row> GetRows(string table, KeySet foreignKey)
        {
            return _dataAdapter.GetRows(table, foreignKey);
        }

        public IEnumerable<string> GetPrimaryColumns(string table)
        {
            return _schema.GetPrimaryColumns(table);
        }
    }
}
