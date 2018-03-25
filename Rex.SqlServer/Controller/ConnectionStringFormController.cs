using Rex.SqlServer.Connection;
using Rex.SqlServer.UI;
using System.Collections.Generic;

namespace Rex.SqlServer.Controller
{
    public class ConnectionStringFormController
    {
        public IConnectionStringForm View;

        private ConnectionStringPersistor _connectionStringPersistor;

        public ConnectionStringFormController()
        {
            _connectionStringPersistor = new ConnectionStringPersistor();
            this.View = new ConnectionParameterWindow(this);
        }

        public string GetConnectionString()
        {
            var connectionString = this.View.GetConnectionString();

            if (connectionString == null)
                return null;

            _connectionStringPersistor.Persist(new SQLConnectionProperties(connectionString.ConnectionString));

            return connectionString.ConnectionString;
        }

        public IList<SQLConnectionProperties> GetPersistedConnections()
        {
            return _connectionStringPersistor.LoadConnections();
        }
    }
}
