using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rex.Business.Store
{
    public class TableGraph
    {
        private TableRelations[,] _tableGraph;
        private int[] visitorGraph;
        private Dictionary<string, int> _tableIndex;
        private IList<string> _tables;

        public void Initialize(InformationSchema informationSchema)
        {
            _tables = informationSchema.GetAllTables();

            _tableGraph = new TableRelations[_tables.Count, _tables.Count];
            visitorGraph = new int[_tables.Count];

            foreach (var table in _tables)
            {
                var referencedTables = informationSchema.GetReferencedTables(table).Select(x => x.Target.Table);
                var stableIndex = _tableIndex[table];

                foreach (var targetTable in referencedTables)
                {
                    if (!_tableIndex.Keys.Contains(targetTable))
                        continue;

                    var targetTableIndex = _tableIndex[targetTable];
                    _tableGraph[stableIndex, targetTableIndex] = TableRelations.OneToOne;
                }

                var referencingTables = informationSchema.GetReferencingTables(table).Select(x => x.Source.Table);

                foreach (var targetTable in referencingTables)
                {
                    if (!_tableIndex.Keys.Contains(targetTable))
                        continue;

                    var targetTableIndex = _tableIndex[targetTable];

                    if (_tableGraph[stableIndex, targetTableIndex] != TableRelations.OneToOne)
                        _tableGraph[stableIndex, targetTableIndex] = TableRelations.OneToMany;
                }
            }
        }
    }
}

