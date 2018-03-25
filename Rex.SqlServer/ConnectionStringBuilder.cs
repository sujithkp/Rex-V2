using System.Data.SqlClient;

namespace Rex.SqlServer
{
    internal class ConnectionStringBuilder
    {
        public string Build(string server, string initialCatalog, string username, string password)
        {
            var connStrBuilder = new SqlConnectionStringBuilder();
            connStrBuilder.DataSource = server;
            connStrBuilder.InitialCatalog = initialCatalog;
            connStrBuilder.UserID = username;
            connStrBuilder.Password = password;

            return connStrBuilder.ConnectionString;
        }

        public string Build(string server, string initialCatalog)
        {
            var connStrBuilder = new SqlConnectionStringBuilder();
            connStrBuilder.DataSource = server;
            connStrBuilder.IntegratedSecurity = true;
            connStrBuilder.InitialCatalog = initialCatalog;

            return connStrBuilder.ConnectionString;
        }
    }
}
