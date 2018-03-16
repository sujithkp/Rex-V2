using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rex.Business.Store
{
    public class TableGraph
    {
        private int _currentDepth;

        private Dictionary<string, Node> NodeIndex = new Dictionary<string, Node>();

        private IList<IList<string>> paths = new List<IList<string>>();

        private int MaxDepth = 10;

        private string _startTable = string.Empty;

        private int CalculateMaxDepth()
        {
            return 5;
            return this.MaxDepth = NodeIndex.
                Select(x => new
                {
                    Name = x.Key,
                    ChildrenCount = x.Value.Nodes.Count
                }).Max(x => x.ChildrenCount);
        }

        public void Initialize(InformationSchema informationSchema)
        {
            var tables = informationSchema.GetAllTables();

            foreach (var table in tables)
            {
                var referencedTables = informationSchema.GetReferencedTables(table).Select(x => x.Target.Table);

                if (!NodeIndex.Keys.Contains(table))
                    NodeIndex.Add(table, new Node(table));

                var thisNode = NodeIndex[table];

                foreach (var targetTable in referencedTables)
                {
                    if (thisNode.ContainsChild(targetTable))
                        continue;

                    if (!this.NodeIndex.Keys.Contains(targetTable))
                        this.NodeIndex.Add(targetTable, new Node(targetTable));

                    thisNode.Nodes.Add(NodeIndex[targetTable]);
                }

                var referencingTables = informationSchema.GetReferencingTables(table).Select(x => x.Source.Table);

                foreach (var targetTable in referencingTables)
                {
                    if (thisNode.ContainsChild(targetTable))
                        continue;

                    if (!this.NodeIndex.Keys.Contains(targetTable))
                        this.NodeIndex.Add(targetTable, new Node(targetTable));

                    thisNode.Nodes.Add(NodeIndex[targetTable]);
                }
            }

            CalculateMaxDepth();
        }

        public IList<string> FindPath(string startTable, string endTable)
        {
            if (_startTable == string.Empty)
                _startTable = startTable;

            if (startTable.Equals(endTable))
                return new String[] { endTable }.ToList();

            List<string> shortestPath = null;

            var startNode = NodeIndex[startTable];
            var endNode = NodeIndex[endTable];


            startNode.Visited = true;
            _currentDepth++;

            if (_currentDepth > MaxDepth)
            {
                _currentDepth--;
                startNode.Visited = false;
                return shortestPath;
            }

            foreach (var node in startNode.Nodes)
            {
                if (node.Name.Equals(startTable))
                    continue;

                if (node.Visited == true)
                    continue;

                if (startTable == _startTable)
                {

                }


                var path = FindPath(node.Name, endTable);



                if (path != null)
                {
                    var p1 = new List<string>();
                    p1.Add(startTable);
                    p1.AddRange(path);

                    if (shortestPath == null)
                        shortestPath = p1;

                    if (p1.Count < shortestPath.Count)
                        shortestPath = p1;

                    if (startTable == _startTable)
                        paths.Add(p1);
                }
            }

            startNode.Visited = false;
            _currentDepth--;

            return shortestPath;
        }

        public IList<IList<string>> FindPaths(string source, string target)
        {
            paths = new List<IList<string>>();

            FindPath(source, target);

            return paths;
        }

    }

    internal class Node
    {
        private bool visited = false;

        public string Name { get; private set; }

        public bool Visited
        {
            set
            {
                visited = value;

                if (value)
                    PathTracer.Add(this.Name);
                else
                    PathTracer.Remove(this.Name);
            }
            get
            {
                return visited;
            }
        }

        public IList<Node> Nodes { get; set; }

        public Node(string name)
        {
            this.Name = name;
            this.Nodes = new List<Node>();
        }

        public bool ContainsChild(string childName)
        {
            return this.Nodes.Any(x => x.Name == childName);
        }
    }


    public class PathTracer
    {
        private static IList<string> path = new List<string>();

        private static StreamWriter writer = new StreamWriter("C:\\logs\\rex.log");

        public static void Add(string node)
        {
            path.Add(node);

            writer.WriteLine(string.Join(" > ", path));
            writer.Flush();
        }

        public static void Remove(string node)
        {
            path.Remove(node);

            writer.WriteLine(string.Join(" > ", path));
            writer.Flush();
        }
    }


}

