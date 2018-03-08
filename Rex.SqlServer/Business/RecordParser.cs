using Rex.Common.Data;
using System.Collections.Generic;
using System.Data;

namespace Rex.SqlServer.Business
{
    public class RecordParser
    {
        public IList<Row> Parse(DataTable dataTable)
        {
            var rows = new List<Row>();

            var columns = dataTable.Columns;

            foreach (DataRow row in dataTable.Rows)
            {
                var r = new Row();

                foreach (DataColumn col in columns)
                    r.Add(col.ColumnName, row[col.ColumnName].ToString());

                rows.Add(r);
            }

            return rows;
        }
    }
}
