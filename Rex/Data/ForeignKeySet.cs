using Rex.Common.Data;
using System.Collections.Generic;

namespace Rex.Common.Data
{
    public class ForeignKeySet : KeySet
    {
        public IList<ColumnValueSet> ForeignKeys { get; private set; }

        public ForeignKeySet()
        {
            this.ForeignKeys = new List<ColumnValueSet>();
        }

    }
}
