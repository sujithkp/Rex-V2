using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace Rex.Common.Data
{
    public class Row
    {
        public IList<ColumnValueSet> Columns { get; private set; }

        public Row()
        {
            this.Columns = new List<ColumnValueSet>();
        }

        public void Add (string columnName, string value)
        {
            this.Columns.Add(new ColumnValueSet(columnName, value));
        }

        public string GetValueFor (string colname)
        {
            return this.Columns.Single(x => x.Name.Equals(colname)).Value;
        }
    }
}
