using System.Collections.Generic;
using System.Linq;

namespace Rex.Business.Store
{
    public class TableGraph
    {
        private TableRelations[,] _tableGraph;
        private bool[] visitorGraph;

        private Dictionary<string, int> _tableIndex;

        private IList<string> _tables;

        public void Initialize(InformationSchema informationSchema)
        {
            _tables = informationSchema.GetAllTables();
            PrepareTableIndex();

            _tableGraph = new TableRelations[_tables.Count, _tables.Count];
            visitorGraph = new bool[_tables.Count];

            foreach (var table in _tables)
            {
                var referencedTables = informationSchema.GetReferencedTables(table).Select(x => x.Target.Table);
                var stableIndex = _tableIndex[table];

                foreach (var targetTable in referencedTables)
                {
                    var targetTableIndex = _tableIndex[targetTable];
                    _tableGraph[stableIndex, targetTableIndex] = TableRelations.OneToOne;
                }

                var referencingTables = informationSchema.GetReferencingTables(table).Select(x => x.Source.Table);

                foreach (var targetTable in referencingTables)
                {
                    var targetTableIndex = _tableIndex[targetTable];

                    if (_tableGraph[stableIndex, targetTableIndex] != TableRelations.OneToOne)
                        _tableGraph[stableIndex, targetTableIndex] = TableRelations.OneToMany;
                }
            }

            // var path = FindPath("Jobs", "Sensors");
            // var path = FindPath("Jobs", "Components");
        }

        public List<string> FindPath(string start, string end)
        {
            if (start == end)
                return null;

            List<string> path = new List<string>();
            visitorGraph[_tableIndex[start]] = true;

            var startTableIndex = _tableIndex[start];

            for (int i = 0; i < _tables.Count; i++)
            {
                var path2 = new List<string>();
                if (_tableIndex[start] == i)
                    continue;

                if (_tableIndex[end] == i && _tableGraph[startTableIndex, i] != 0)
                {
                    path2.Add(start);
                    path2.Add(end);
                    visitorGraph[_tableIndex[start]] = false;

                    return path2;
                }

                if (_tableGraph[startTableIndex, i] == 0)
                    continue;

                if (visitorGraph[i] == true)
                    continue;

                var path3 = FindPath(_tables[i], end);

                if (path3.Count != 0)
                {
                    path2.Add(start);
                    path2.AddRange(path3);
                }

                if (path == null || path.Count == 0)
                    path = path2;

                if (path.Count > 0 && path2.Count > 0)
                    if (path2.Count < path.Count)
                        path = path2;
            }

            visitorGraph[_tableIndex[start]] = false;
            return path;
        }

        private void PrepareTableIndex()
        {
            _tableIndex = new Dictionary<string, int>();

            var index = 0;
            foreach (var table in _tables)
                _tableIndex.Add(table, index++);
        }
    }
}
