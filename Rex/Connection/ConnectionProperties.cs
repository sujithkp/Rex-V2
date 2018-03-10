namespace Rex.Common.Connection
{
    public class ConnectionProperties
    {
        public ConnectionProperties(string detail)
        {
            this.ConnectionDetail = detail;
        }

        public string ConnectionDetail { get; private set; }
    }
}
