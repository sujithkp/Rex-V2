using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Rex.SqlServer.UI
{
    public partial class ConnectionStringForm : Form
    {
        public ConnectionStringForm()
        {
            InitializeComponent();
        }

        public SqlConnectionStringBuilder ConnectionString { get; private set; }

        public string ConnectionStringName { get; private set; }

        public bool Verified { get; private set; }

        private void btnOk_click(object sender, EventArgs e)
        {
            var ConnectionStringName = txtConnectionStrinName.Text;
            var ServerName = txtServerName.Text;
            var Username = txtLogin.Text;
            var Password = txtPassword.Text;
            var DatabaseName = txtDatabase.Text;

            var connectionStringBuilder = new ConnectionStringBuilder();
            var connectionString = connectionStringBuilder.Build(ServerName, DatabaseName, Username, Password);

            if (Verify(connectionString))
            {
                this.ConnectionString = new SqlConnectionStringBuilder(connectionString);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private bool Verify(string connectionString)
        {
            var sqlClient = new System.Data.SqlClient.SqlConnection(connectionString);

            try
            {
                sqlClient.Open();

                if (sqlClient.State != System.Data.ConnectionState.Open)
                    throw new Exception("Could not connect.");

                sqlClient.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
    }
}
