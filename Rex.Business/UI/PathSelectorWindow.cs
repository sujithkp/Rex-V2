using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Rex.Business.UI
{
    public partial class PathSelectorWindow : Form, IPathSelector
    {
        public PathSelectorWindow()
        {
            InitializeComponent();
        }

        private string _selectedPath = null;

        public string GetSelectedPath(IList<string> paths)
        {
            _selectedPath = null;

            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange(paths.ToArray());
            this.listBox1.SelectedIndex = 0;

            if (this.ShowDialog() != DialogResult.OK)
                return null;

            return _selectedPath;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedPath = listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }
    }
}
