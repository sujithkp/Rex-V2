using Rex.SqlServer.Controller;
using System.Linq;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using Rex.SqlServer.Connection;

namespace Rex.SqlServer.UI
{
    public partial class ConnectionParameterWindow : Form
    {
        public ConnectionParameterWindow(ConnectionStringFormController controller)
        {
            InitializeComponent();

            this.Controller = controller;

            _connections = this.Controller.GetPersistedConnections();

            _connections.Select(x => x.DataSource).Distinct().ToList().ForEach(x => cmbDataSource.Items.Add(x));

            cmbDataSource.SelectedIndex = 0;
            cmbDataSource.SelectAll();

            cmbDataSource.Focus();
        }

        private IList<SQLConnectionProperties> _connections;

        public ConnectionStringFormController Controller { get; set; }

        private SqlConnectionStringBuilder ConnectionString { get; set; }

        public SqlConnectionStringBuilder GetConnectionString()
        {
            if (this.ShowDialog() == DialogResult.OK)
                return this.ConnectionString;

            return null;
        }

        private void btnOk_click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            var connectionString = string.Empty;
            var connectionStringBuilder = new ConnectionStringBuilder();

            var serverName = cmbDataSource.Text;
            var dbName = cmbDatabase.Text;

            if (string.IsNullOrWhiteSpace(dbName))
            {
                MessageBox.Show("Database field cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbAuthType.SelectedIndex == 0)
                connectionString = connectionStringBuilder.Build(serverName, dbName, cmbUserId.Text.Trim(), txtPassword.Text.Trim());
            else
                connectionString = connectionStringBuilder.Build(serverName, dbName);

            if (Verify(connectionString))
            {
                this.ConnectionString = new SqlConnectionStringBuilder(connectionString);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ConnectionString = null;

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

        private void ConnectionParameterWindow_Load(object sender, EventArgs e)
        {
            cmbAuthType.SelectedIndex = 0;
        }

        private void cmbAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthType.SelectedIndex == 0)
            {
                cmbUserId.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                cmbUserId.Enabled = false;
                txtPassword.Enabled = false;
            }
        }

        private void ConnectionParameterWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (this.DialogResult == DialogResult.None);
        }

        private void cmbUserId_DropDown(object sender, EventArgs e)
        {
            cmbUserId.Items.Clear();

            var dataSource = cmbDataSource.Text.Trim();

            _connections.Where(x => x.DataSource.ToLower().Equals(dataSource))
                .Select(x => x.UserId).ToList()
                .ForEach(x => cmbUserId.Items.Add(x));
        }

        private void txtDatabase_DropDown(object sender, EventArgs e)
        {
            cmbDatabase.Items.Clear();

            var dataSource = cmbDataSource.Text.Trim();

            _connections.Where(x => x.DataSource.ToLower().Equals(dataSource))
                .Select(x => x.DataBase).ToList()
                .ForEach(x => cmbDatabase.Items.Add(x));
        }

        private void cmbUserId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dataSource = cmbDataSource.Text.Trim();
            var userId = cmbUserId.Text.Trim();

            var password = _connections.Where(x => x.DataSource.ToLower().Equals(dataSource) && x.UserId.Equals(userId))
                 .Select(x => x.PassWord)
                 .FirstOrDefault();

            txtPassword.Text = password;
        }

        private void txtPassword_GotFocus(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }
    }
}
