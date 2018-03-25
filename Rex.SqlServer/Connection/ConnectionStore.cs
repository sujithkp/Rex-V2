using System.Collections.Generic;

namespace Rex.SqlServer.Connection
{
    public class ConnectionStore
    {
        public List<SQLConnectionProperties> Connections { get; set; }

        public ConnectionStore()
        {
            this.Connections = new List<SQLConnectionProperties>();
        }
    }
}
