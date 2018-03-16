using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Rex.Business.Store
{
    public class TableGraph
    {
        private Dictionary<string, Node> NodeIndex = new Dictionary<string, Node>();

        private int MaxDepth = 20;

        public void Initialize(InformationSchema informationSchema)
        {
            var tables = informationSchema.GetAllTables();//.Where(x => !x.Contains("Par")).ToList(); ;

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
        }

        public void TestGetPath()
        {

            //NodeIndex.Select(x => new { Name = x.Key, Count = x.Value.Nodes.Count })
            //    .OrderBy(x => x.Count).ToList()
            //    .ForEach(x => Console.WriteLine(x.Name + " : " + x.Count));


            //Console.ReadKey();

            //return;

            var startTable = "LoanRequest";
            var endTable = "Payment";

            ShortestPathLength = null;
            startTableName = startTable;

            var startTime = DateTime.Now;
            var path = FindPath(startTable, endTable);
            var timeTaken = (DateTime.Now.Subtract(startTime)); ;


            var numberofPathsFound = pathsFound.Count;

            System.Diagnostics.Debug.Print(timeTaken.TotalSeconds.ToString());

        }

        private string startTableName = string.Empty;

        private List<IList<string>> pathsFound = new List<IList<string>>();

        private int? ShortestPathLength;

        private void ShowNodeEntered(string name)
        {
            Console.SetCursorPosition(5, 10);
            Console.Write(name);
            Console.Write("                                                                                           ");
            Thread.Sleep(500);
        }

        private int Depth;

        public IList<string> FindPath(string startTable, string endTable)
        {
            if (startTable.Equals(endTable))
                return new String[] { endTable }.ToList();

            List<string> shortestPath = null;

            var startNode = NodeIndex[startTable];
            var endNode = NodeIndex[endTable];

            startNode.Visited = true;
            Depth++;

            if (Depth > MaxDepth)
            {
                Depth--;
                return shortestPath;
            }

            foreach (var node in startNode.Nodes)
            {
                if (node.Name.Equals(startTable))
                    continue;

                if (node.Visited == true)
                    continue;

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
                }
            }

            startNode.Visited = false;
            Depth--;
            return shortestPath;
        }
    }

    internal class Node
    {
        public string Name { get; private set; }

        public bool Visited { get; set; }

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




}

