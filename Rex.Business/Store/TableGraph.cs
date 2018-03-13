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

        private StreamWriter writer = new StreamWriter("c:\\logs\\rex.log");

        private int visitorCount = 0;

        public void Initialize(InformationSchema informationSchema)
        {
            _tables = informationSchema.GetAllTables().Where(x => !x.Contains("ParMap")).ToList();
            PrepareTableIndex();

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

            var pathList = new List<List<string>>();
            var baseTable = "LoanRequest";

            var selectedTables = _tables;

            foreach (var table in selectedTables)
            {
                if (table == baseTable)
                    continue;

                writer.WriteLine("looking for relation between " + baseTable + " and " + table);
                writer.Flush();

                var path = FindPath(string.Empty, baseTable, table);
                pathList.Add(path);
            }

            // var path = FindPath("Jobs", "Components");
        }


        private string prepareVisitorGraph()
        {
            StringBuilder tables = new StringBuilder();
            try
            {
                for (int i = 1; i <= visitorCount; i++)
                {
                    var tableIndex = Array.IndexOf(visitorGraph, i);
                    tables.Append(" > " + _tables[tableIndex]);
                }

                writer.WriteLine(tables.ToString());
                writer.Flush();
            }
            catch (Exception ex)
            {

            }

            return tables.ToString();
        }

        public List<string> FindPath(string pathFollowed, string start, string end)
        {
            if (start == end)
                return null;

            List<string> path = new List<string>();
            visitorGraph[_tableIndex[start]] = ++visitorCount;
            prepareVisitorGraph();

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
                    visitorGraph[_tableIndex[start]] = 0;
                    --visitorCount;
                    prepareVisitorGraph();


                    return path2;
                }

                if (_tableGraph[startTableIndex, i] == 0)
                    continue;

                if (visitorGraph[i] > 0)
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

                if (start == "LoanRequest")
                {

                }
            }

            visitorGraph[_tableIndex[start]] = 0;
            --visitorCount;
            prepareVisitorGraph();

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
