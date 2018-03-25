using Rex.Common.Connection;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace Rex.SqlServer.Connection
{
    public class SQLConnectionProperties : ConnectionProperties
    {
        public SQLConnectionProperties()
        {

        }

        public SQLConnectionProperties(string connectionString)
        {
            var connectionBuilder = new SqlConnectionStringBuilder(connectionString);

            this.DataSource = connectionBuilder.DataSource;
            this.DataBase = connectionBuilder.InitialCatalog;
            this.UserId = connectionBuilder.UserID;
            this.PassWord = connectionBuilder.Password;
            this.IntegratedSecurity = connectionBuilder.IntegratedSecurity;
        }

        public string DataSource { get; set; }

        public string DataBase { get; set; }

        public string UserId { get; set; }

        public string PassWord { get; set; }

        public bool IntegratedSecurity { get; set; }

        [XmlIgnore]
        public override string ConnectionDetail
        {
            get
            {
                return this.DataBase + " @ " + DataSource;
            }
        }
    }
}
