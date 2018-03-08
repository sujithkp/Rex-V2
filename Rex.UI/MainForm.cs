using Rex.Common.Data;
using Rex.UI.Controllers;
using Rex.UI.Controls;
using Rex.UI.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Rex.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.controller = new MainFormController();

            treeView1.BeforeExpand += TreeView1_BeforeExpand;
        }

        private MainFormController controller;

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (!e.Node.Nodes[0].Text.Equals("."))
                return; // Node is already populated.

            e.Node.Nodes[0].Remove();
            IList<RexNode> dependants = null;

            if (e.Node is TableNode)
                dependants = controller.GetDependants(e.Node as TableNode);

            if (e.Node is TableCollectionNode)
                dependants = controller.GetDependants(e.Node as TableCollectionNode);

            e.Node.Nodes.AddRange(dependants.ToArray());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var keySet = new PrimaryKeySet();
            keySet.PrimaryKeys.Add(new ColumnValueSet("EmployeeID", "9"));


            treeView1.Nodes.Add(new TableNode("Employees","Employees", keySet));


        }
    }
}
