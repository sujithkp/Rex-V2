using Rex.Common.Data;
using System.Collections.Generic;
using System.Data;

namespace Rex.SqlServer.Business
{
    internal class ReferentialConstraintParser
    {
        public IEnumerable<ReferentialConstraint> Parse(DataTable table)
        {
            var referentialConstraints = new List<ReferentialConstraint>();
            var refConstraintsdict = new Dictionary<string, List<TableColumnPair>>();

            foreach (DataRow row in table.Rows)
            {
                var constraintName = row["FK_Name"].ToString();
                var sourceTable = row["FK_Table"].ToString();
                var sourceCol = row["FK_Column"].ToString();
                var targetTable = row["PK_Table"].ToString();
                var targetCol = row["PK_Column"].ToString();

                if (!refConstraintsdict.ContainsKey(constraintName))
                    refConstraintsdict.Add(constraintName, new List<TableColumnPair>());

                refConstraintsdict[constraintName].Add(new TableColumnPair()
                {
                    Source = new TableColumn(sourceTable, sourceCol),
                    Target = new TableColumn(targetTable, targetCol)
                });
            }

            foreach (var item in refConstraintsdict)
            {
                var refConst = new ReferentialConstraint(item.Key);

                foreach (var tblsCols in item.Value)
                    refConst.AddTableColumn(tblsCols);

                referentialConstraints.Add(refConst);
            }

            return referentialConstraints;
        }
    }
}
