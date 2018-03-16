using Rex.Business.Store;
using Rex.Common;
using Rex.Common.Connection;
using Rex.Common.Data;
using System.Collections.Generic;

namespace Rex.Business
{
    public class DataStore
    {
        private IDbAdapter _dataAdapter;
        private InformationSchema _schema;
        private TableGraph _tableGraph;

        public DataStore()
        {
            _schema = new InformationSchema();
        }

        void Initialize()
        {
            var referentialConstraints = _dataAdapter.GetReferentialConstraints();
            _schema.Initialize(referentialConstraints);

            (_tableGraph = new TableGraph()).Initialize(_schema);
        }

        public ConnectionProperties Connect()
        {
            _dataAdapter = new SqlServer.Adapter.SqlServerAdapter();
            var connectionProperties = _dataAdapter.Connect();

            if (connectionProperties == null)
                return null;

            Initialize();

            return connectionProperties;
        }

        public TablePrimaryKeys GetNewRecordKeys()
        {
            return new Controller.NewRecordFormController(_schema).GetRecordPrimaryKeySet();
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

        public IEnumerable<string> GetAllTables()
        {
            return _schema.GetAllTables();
        }

        public IEnumerable<string> GetForeignKeyColumns(string table)
        {
            return _schema.GetForeignKeyColumns(table);
        }

        public IEnumerable<string> FindPath(string source, string target)
        {
            return _tableGraph.FindPath(source, target);
        }
    }
}
