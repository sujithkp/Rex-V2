using Rex.Common.Connection;
using Rex.Common.Data;
using Rex.UI.Controllers;
using Rex.UI.Controls;
using Rex.UI.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        private bool IsConnected { get; set; }

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
                dataGridView1.AllowUserToAddRows = false;
            }
        }

        private void ShowChildren(IEnumerable<Row> rows)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            if (rows == null || rows.Count() == 0)
                return;

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

        private void LoadSettings()
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

            if (dependants != null)
                e.Node.Nodes.AddRange(dependants.ToArray());
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Record Explorer (Rex)" + Environment.NewLine
               + "Author: Sujith K P"
               + Environment.NewLine
               + "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString()
               , "About me.");
        }

        private void sqlServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionProperties connectionDetail = null;
            var connectTask = Task<ConnectionProperties>.Factory.StartNew(() => controller.Connect());
            connectionDetail = connectTask.Result;

            conectToolStripMenuItem.Enabled = !(this.IsConnected = (connectionDetail != null));

            if (connectionDetail == null)
                return;

            this.Text = "Rex - " + connectionDetail.ConnectionDetail;

            if (!conectToolStripMenuItem.Enabled)
                statusStrip1.Items[0].Text = "Connected";
        }

        private void addRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.IsConnected)
            {
                sqlServerToolStripMenuItem_Click(sender, e);

                if (this.IsConnected)
                    addRecordToolStripMenuItem_Click(sender, e);

                return;
            }

            var pKeys = controller.AddRecord();

            if (pKeys == null)
                return;

            treeView1.Nodes.Add(new TableNode(pKeys.TableName, pKeys.TableName, pKeys.PrimaryKeys));
        }
    }
}
