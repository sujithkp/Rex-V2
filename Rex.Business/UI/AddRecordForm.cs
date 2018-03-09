using Rex.Business.Controller;
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

        public AddRecordForm(NewRecordFormController controller)
        {
            InitializeComponent();
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

        }
    }
}
