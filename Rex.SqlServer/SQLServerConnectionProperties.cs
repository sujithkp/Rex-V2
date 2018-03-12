using Rex.Common.Connection;

namespace Rex.SqlServer
{
    public class SQLServerConnectionProperties : ConnectionProperties
    {
        public override string ConnectionDetail
        {
            get
            {
                return this.DataBase + " @ " + this.DataSource;
            }
        }

        public SQLServerConnectionProperties(string name, string dataSource, string dataBase, string username, string password)
        {
            this.ConnectionName = name;
            this.DataSource = DataSource;
            this.DataBase = DataBase;
            this.UserName = username;
            this.Password = password;
        }

        public string DataSource { get; private set; }

        public string DataBase { get; private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string ConnectionName { get; private set; }
    }
}
