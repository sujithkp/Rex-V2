using System.Collections;
using System.Collections.Generic;

namespace Rex.Common.Data
{
    public class ReferentialConstraint
    {
        private IList<TableColumnPair> PColumnPairs { get; set; }

        public string Name { get; private set; }

        public IEnumerable<TableColumnPair> Participators { get { return PColumnPairs; } }

        public ReferentialConstraint (string name)
        {
            this.Name = name;
            this.PColumnPairs = new List<TableColumnPair>();
        }

        public ReferentialConstraint AddTableColumn (TableColumnPair tblColPair)
        {
            this.PColumnPairs.Add(tblColPair);

            return this;
        }

    }
}
