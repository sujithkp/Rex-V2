using Rex.Business.Store;
using Rex.Common;
using Rex.Common.Data;
using System;
using System.Collections.Generic;

namespace Rex.Business
{
    public class DataStore
    {
        private IDbAdapter _dataAdapter;
        private InformationSchema _schema;

        public DataStore()
        {
            //_dataAdapter = new SqlServer.Adapter.SqlServerAdapter("Data Source=SVRSQL1;Initial Catalog=Northwind;Persist Security Info=True;User ID=MdomUser;Password=HHeLiBe1");
            _schema = new InformationSchema();

            //Initialize();
        }

        void Initialize()
        {
            _schema.Initialize(_dataAdapter.GetReferentialConstraints());
        }

        public void Connect ()
        {
            _dataAdapter = new SqlServer.Adapter.SqlServerAdapter();
            _dataAdapter.Connect();

            Initialize();
        }

        public IList<TableColumnPair> GetTablesReferencedBy (string tableName)
        {
            return _schema.GetReferencedTables(tableName);
        }

        public IList<TableColumnPair> GetTablesReferencing(string tableName)
        {
            return _schema.GetReferencingTables(tableName);
        }

        public Row GetRow (string tableName, KeySet primaryKey)
        {
            return _dataAdapter.GetRow(tableName, primaryKey);
        }

        public IEnumerable<Row> GetRows (string table, KeySet foreignKey)
        {
            return _dataAdapter.GetRows(table, foreignKey);
        }

        public IEnumerable<string> GetPrimaryColumns(string table)
        {
            return _schema.GetPrimaryColumns(table);
        }
    }
}
