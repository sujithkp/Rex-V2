using Rex.Common.Data;
using System.Linq;

namespace Rex.SqlServer.Store
{
    public static class DataQueries
    {
        private static string _rowQuery = "Select * from [{0}] where {1};";

        public static string GetRowQuery(string table, PrimaryKeySet primaryKeyset)
        {
            var whereClause = string.Join(" and ", primaryKeyset.PrimaryKeys.Select(x => x.Name + "=" + "'" + x.Value + "'"));

            return string.Format(_rowQuery, table, whereClause);
        }

        public static string GetRowQuery(string table, ForeignKeySet primaryKeyset)
        {
            var whereClause = string.Join(" and ", primaryKeyset.ForeignKeys.Select(x => x.Name + "=" + "'" + x.Value + "'"));

            return string.Format(_rowQuery, table, whereClause);
        }
    }
}
