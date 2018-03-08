using Rex.Common.Data;
using System.Collections.Generic;

namespace Rex.Common.Data
{
    public class PrimaryKeySet : KeySet
    {
        public IList<ColumnValueSet> PrimaryKeys { get; private set; }

        public PrimaryKeySet()
        {
            this.PrimaryKeys = new List<ColumnValueSet>();
        }

    }
}
