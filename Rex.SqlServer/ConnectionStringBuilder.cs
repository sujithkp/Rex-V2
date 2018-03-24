namespace Rex.SqlServer
{
    internal class ConnectionStringBuilder
    {
        //private string connectionStringFormat = "Data Source={0};Initial Catalog={1};Persist Security Info=True;Integrated Security=SSPI";

        private string connectionStringFormat = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";

        public string Build(string server, string initialCatalog, string username, string password)
        {
            return string.Format(connectionStringFormat, server, initialCatalog, username, password);
        }
    }
}
