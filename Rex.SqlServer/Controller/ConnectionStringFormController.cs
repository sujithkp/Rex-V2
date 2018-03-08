using Rex.SqlServer.Business;
using Rex.SqlServer.UI;

namespace Rex.SqlServer.Controller
{
    public class ConnectionStringFormController
    {
        public string GetConnectionString()
        {
            var form = new ConnectionStringForm();
            var dialogResult = form.ShowDialog();

            var connectionStringName = form.ConnectionStringName;
            var serverName = form.ServerName;
            var username = form.Username;
            var password = form.Password;
            var database = form.DatabaseName;

            if (dialogResult != System.Windows.Forms.DialogResult.OK)
                return null;

            new ConnectionStringPersistor().Persist(connectionStringName, serverName, username, password);

            return new ConnectionStringBuilder().Build(serverName, database, username, password);
        }
    }
}
