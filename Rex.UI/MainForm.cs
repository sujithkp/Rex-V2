using Rex.Common.Data;
using Rex.UI.Controllers;
using Rex.UI.Controls;
using Rex.UI.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rex.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadSettings();

            this.Load += MainForm_Load1;
        }

        private void MainForm_Load1(object sender, EventArgs e)
        {
            this.controller = new MainFormController();

            treeView1.BeforeExpand += TreeView1_BeforeExpand;
            treeView1.BeforeSelect += TreeView1_BeforeSelect;
            treeView1.AfterSelect += TreeView1_AfterSelect;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is TableCollectionNode)
            {
                var rows = controller.GetRows(e.Node as TableCollectionNode);
                ShowChildren(rows);
                return;
            }

            if (e.Node is TableNode)
            {
                var task = Task.Factory.StartNew(() => controller.GetRows(e.Node as TableNode));

                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                dataGridView1.Columns.Add("Name", "Name");
                dataGridView1.Columns.Add("Value", "Value");

                var row = task.Result;

                foreach (var col in row.Columns)
                    dataGridView1.Rows.Add(new string[] { col.Name, col.Value });

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void ShowChildren(IEnumerable<Row> rows)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            foreach (var col in rows.First().Columns)
                dataGridView1.Columns.Add(col.Name, col.Name);

            foreach (var row in rows)
            {
                var rowValues = row.Columns.Select(x => x.Value).ToArray();
                dataGridView1.Rows.Add(rowValues);
            }
        }

        private void TreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node as RexNode;
            statusStrip1.Items[0].Text = node.ToString();
        }

        private MainFormController controller;

        private void LoadSettings ()
        {
            singularizeToolStripMenuItem.Checked = Properties.Settings.Default.Singularize;
        }

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

        private void singularizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            singularizeToolStripMenuItem.Checked = !singularizeToolStripMenuItem.Checked;
            Properties.Settings.Default.Singularize = singularizeToolStripMenuItem.Checked;

            Properties.Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
