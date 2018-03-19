using Rex.SqlServer.Business;
using Rex.SqlServer.UI;

namespace Rex.SqlServer.Controller
{
    public class ConnectionStringFormController
    {
        public string GetConnectionString()
        {
            var form = new ConnectionParameterWindow();
            var dialogResult = form.ShowDialog();

            var connectionString = form.ConnectionString;
            if (connectionString == null)
                return null;

            var connectionStringName = form.ConnectionStringName;

            if (dialogResult != System.Windows.Forms.DialogResult.OK)
                return null;

            new ConnectionStringPersistor().Persist(connectionStringName, connectionString.DataSource,
                connectionString.UserID, connectionString.Password);

            return connectionString.ConnectionString;
        }
    }
}
