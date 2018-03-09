using System;
using System.Windows.Forms;

namespace Rex.SqlServer.UI
{
    public partial class ConnectionStringForm : Form
    {
        public ConnectionStringForm()
        {
            InitializeComponent();
        }

        public string ConnectionStringName { get; private set; }

        public string ServerName { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string DatabaseName { get; private set; }

        private void btnOk_click(object sender, EventArgs e)
        {
            this.ConnectionStringName = txtConnectionStrinName.Text;
            this.ServerName = txtServerName.Text;
            this.Username = txtLogin.Text;
            this.Password = txtPassword.Text;
            this.DatabaseName = txtDatabase.Text;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
