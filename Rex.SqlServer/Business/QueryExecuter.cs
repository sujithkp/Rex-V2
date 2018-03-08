using System.Data;
using System.Data.SqlClient;

namespace Rex.SqlServer.Business
{
    internal class QueryExecuter
    {
        public QueryExecuter(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        private System.Data.SqlClient.SqlConnection _connection;

        public DataTable Execute (string sql)
        {
            var command = new SqlCommand(sql, _connection);

            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();

            DataTable dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader(), LoadOption.OverwriteChanges);

            _connection.Close();

            return dataTable;
        }
    }
}
