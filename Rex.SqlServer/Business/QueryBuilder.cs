using Rex.Common;
using Rex.Common.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rex.SqlServer.Business
{
    public class QueryBuilder
    {
        public string BuildInnerJoinQuery(string[] path, List<TableColumnPair> relations, KeySet primaryKeySet,
            IEnumerable<string> targetTablePrimaryCols)
        {
            path.Reverse();

            var finalTable = path[path.Length - 1];
            var finalTableAlias = "_" + finalTable;

            var firstTable = path[0];
            var firstTableAlias = "_" + firstTable;

            var targetCols = targetTablePrimaryCols.Select(x => finalTableAlias + "." + x).ToArray();

            //When the table has no primary columns. we need to get the columns in the table which are refered from other tables.
            if (targetCols.Count() == 0)
            {
                targetCols = relations.Where(x => x.Target.Table == finalTable).Select(x => finalTableAlias + "." + x.Target.Column).ToArray();

                if (targetCols.Count() == 0)
                    targetCols = relations.Where(x => x.Source.Table == finalTable).Select(x => finalTableAlias + "." + x.Source.Column).ToArray();
            }

            var query = new StringBuilder();
            query.Append("Select DISTINCT" + " " + string.Join(",", targetCols) + " FROM " + firstTable + " " + firstTableAlias);

            for (int i = 0; i < path.Length - 1; i++)
            {
                var filteredRelation = FilterRelation(relations, path[i], path[i + 1]);
                var innerJoinStmnt = BuildInnerJoinStr(path[i], path[i + 1], filteredRelation);
                query.Append(" " + innerJoinStmnt);
            }

            query.Append(" " + BuildWhereCriteria(firstTableAlias, primaryKeySet as PrimaryKeySet) + ";");

            return query.ToString();
        }

        private string BuildWhereCriteria(string finalTableAlias, PrimaryKeySet primaryKeys)
        {
            var strBuilder = new StringBuilder();
            var criterias = new List<string>();

            strBuilder.Append("WHERE ");

            foreach (var key in primaryKeys.PrimaryKeys)
                criterias.Add(finalTableAlias + "." + key.Name + " = '" + key.Value + "'");

            strBuilder.Append(string.Join(" and ", criterias));

            return strBuilder.ToString();

        }


        private IList<TableColumnPair> FilterRelation(List<TableColumnPair> relations, string sourceTable, string targetTable)
        {
            var list1 = relations.Where(x => x.Source.Table == sourceTable && x.Target.Table == targetTable).ToList();

            if (list1.Count() != 0)
                return list1;

            return relations.Where(x => x.Target.Table == sourceTable && x.Source.Table == targetTable).ToList();
        }

        private string BuildInnerJoinStr(string sourceTable, string targetTable, IList<TableColumnPair> relations)
        {
            var str = new StringBuilder();
            var targetTableAlias = "_" + targetTable.Replace(" ", string.Empty);
            var sourceTableAlias = "_" + sourceTable;

            str.Append("INNER JOIN [" + targetTable + "] " + targetTableAlias + " ON ");

            foreach (var relation in relations)
                str.Append("_" + relation.Source.Table.Replace(" ", string.Empty) + "." + relation.Source.Column
                    + " = " + "_" + relation.Target.Table.Replace(" ", string.Empty) + "." + relation.Target.Column);

            return str.ToString();
        }

    }
}
