using Rex.Business.Controller;
using Rex.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rex.Business.UI
{
    public partial class AddRecordForm : Form
    {
        private NewRecordFormController Controller;
        public PrimaryKeySet PrimaryKeys { get; private set; }
        public String Table { get; private set; }

        public TablePrimaryKeys Result { get; private set; }

        public AddRecordForm(NewRecordFormController controller)
        {
            InitializeComponent();

            this.Controller = controller;
        }

        public PrimaryKeySet GetPrimaryKeys(IEnumerable<String> tables)
        {
            tables.ToList().ForEach(x => this.comboBox1.Items.Add(x));
            var result = this.ShowDialog();

            if (result != DialogResult.OK)
                return null;

            return this.PrimaryKeys;
        }

        public void ShowPrimaryColumns(IList<String> columns)
        {
            this.panel1.VerticalScroll.Enabled = true;
            this.panel1.Show();

            var topMargin = 5;
            var bottomMargin = 10;
            var startTop = 10;

            var txtBoxleftMargin = 500;

            for (int i = 0; i < 20; i++)
            {
                var txtbox = new TextBox() { Top = startTop, Left = txtBoxleftMargin, Width = 100 };
                startTop += topMargin + txtbox.Height + bottomMargin;

                this.panel1.Controls.Add(txtbox);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedTable = comboBox1.Text;

            PrimaryKeySet primaryKeys = new PrimaryKeySet();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var colName = row.Cells[0].Value.ToString();
                var colValue = row.Cells[1].Value.ToString();

                primaryKeys.PrimaryKeys.Add(new ColumnValueSet(colName, colValue));
            }

            this.PrimaryKeys = primaryKeys;
            this.Table = comboBox1.Text;

            this.Result = new TablePrimaryKeys(this.Table, this.PrimaryKeys);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cols = Controller.GetPrimaryKeys(comboBox1.Text);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Rows.Clear();

            foreach (var col in cols)
                dataGridView1.Rows.Add(new string[] { col, string.Empty });
        }
    }
}
