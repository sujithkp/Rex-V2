using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rex.Business.Store
{
    public class TableGraph
    {
        private Dictionary<string, Node> NodeIndex = new Dictionary<string, Node>();

        public void Initialize(InformationSchema informationSchema)
        {
            var tables = informationSchema.GetAllTables();

            foreach (var table in tables)
            {
                var referencedTables = informationSchema.GetReferencedTables(table).Select(x => x.Target.Table);
                var thisNode = new Node(table);
                NodeIndex.Add(table, thisNode);

                foreach (var targetTable in referencedTables)
                {
                    if (!thisNode.ContainsChild(targetTable))
                        continue;

                    if (!this.NodeIndex.Keys.Contains(targetTable))
                        this.NodeIndex.Add(targetTable, new Node(targetTable));

                    thisNode.Nodes.Add(NodeIndex[targetTable]);
                }

                var referencingTables = informationSchema.GetReferencingTables(table).Select(x => x.Source.Table);

                foreach (var targetTable in referencingTables)
                {
                    if (!thisNode.ContainsChild(targetTable))
                        continue;

                    if (!this.NodeIndex.Keys.Contains(targetTable))
                        this.NodeIndex.Add(targetTable, new Node(targetTable));

                    thisNode.Nodes.Add(NodeIndex[targetTable]);
                }
            }
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

