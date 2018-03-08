using Rex.Common.Data;
using System;
using System.Collections.Generic;

namespace Rex.Common
{
    public interface IDbAdapter
    {
        IEnumerable<String> GetAllTables();

        IEnumerable<ReferentialConstraint> GetReferentialConstraints();

        Row GetRow(string tableName, KeySet primaryKey);

        IEnumerable<Row> GetRows(string tableName, KeySet foreignKey);

        void Connect();
    }
}
