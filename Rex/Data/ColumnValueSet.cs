using System;

namespace Rex.Common.Data
{
    public class ColumnValueSet
    {
        public ColumnValueSet(String colName, string colValue)
        {
            this.Name = colName;
            this.Value = colValue;
        }

        public String Name { get; private set; }

        public String Value { get; private set; }
    }
}
