using Rex.Common;
using Rex.Common.Data;
using Rex.SqlServer.Business;
using Rex.SqlServer.Store;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rex.SqlServer.Adapter
{
    public class SqlServerAdapter : IDbAdapter
    {
        private QueryExecuter _queryExecuter;

        private ReferentialConstraintParser _refparser;

        private RecordParser _recordParser;

        public SqlServerAdapter(string connectionString)
        {
            _queryExecuter = new QueryExecuter(connectionString);
            _refparser = new ReferentialConstraintParser();
            _recordParser = new RecordParser();
        }

        public IEnumerable<string> GetAllTables()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReferentialConstraint> GetReferentialConstraints()
        {
            var table = _queryExecuter.Execute(SchemaQueries.ReferentialConstraintQuery);
            return _refparser.Parse(table);
        }

        public Row GetRow(string tableName, KeySet primaryKey)
        {
            var row = _queryExecuter.Execute(DataQueries.GetRowQuery(tableName, primaryKey as PrimaryKeySet));
            return _recordParser.Parse(row).First();
        }

        public IEnumerable<Row> GetRows(string tableName, KeySet foreignKey)
        {
            var rows = _queryExecuter.Execute(DataQueries.GetRowQuery(tableName, foreignKey as ForeignKeySet));
            return _recordParser.Parse(rows);
        }
    }
}
