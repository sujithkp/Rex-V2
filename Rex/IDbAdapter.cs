using Rex.Common.Data;
using System.Collections.Generic;

namespace Rex.Common
{
    public interface IDbAdapter
    {
        IEnumerable<string> GetAllTables();

        IEnumerable<ReferentialConstraint> GetReferentialConstraints();

        Row GetRow(string tableName, KeySet primaryKey);

        IEnumerable<Row> GetRows(string tableName, KeySet foreignKey);

        bool Connect();
    }
}
