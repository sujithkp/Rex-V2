using System.Data.SqlClient;

namespace Rex.SqlServer.UI
{
    public interface IConnectionStringForm
    {
        SqlConnectionStringBuilder GetConnectionString();
    }
}
