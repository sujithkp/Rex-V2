using Rex.Business.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Rex.Business.UI
{
    public partial class TableSelectorWindow : Form, ITableSelector
    {
        private TableSelectorController _controller;
        private string _selectedTable;

        public TableSelectorWindow()
        {
            InitializeComponent();
        }

        public string GetSelectedTable(IList<string> tables)
        {
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(tables.OrderBy(x => x).ToArray());
            this.comboBox1.SelectedIndex = 0;

            this.ShowDialog();

            return _selectedTable;
        }

        public TableSelectorWindow(TableSelectorController controller)
        {
            _controller = controller;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _selectedTable = this.comboBox1.Text;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
