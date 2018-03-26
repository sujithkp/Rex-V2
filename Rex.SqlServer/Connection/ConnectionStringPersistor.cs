using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Rex.SqlServer.Connection
{
    public class ConnectionStringPersistor
    {
        private ConnectionStore _store = new ConnectionStore();

        private string appDataPath = Environment.SpecialFolder.ApplicationData + "\\Rex" + "\\SqlServer";

        public void Persist(SQLConnectionProperties connectionProperties)
        {
            var alreadyExist = _store.Connections.Any(x =>
                x.DataSource == connectionProperties.DataSource
                && x.DataBase == connectionProperties.DataBase
                && x.IntegratedSecurity == connectionProperties.IntegratedSecurity
                && connectionProperties.UserId == connectionProperties.UserId
                && connectionProperties.PassWord == x.PassWord
            );

            if (alreadyExist)
                return;

            _store.Connections.Add(connectionProperties);

            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            var streamWriter = new StreamWriter(appDataPath + "\\Connections.txt");
            XmlSerializer serializer = new XmlSerializer(typeof(ConnectionStore));
            serializer.Serialize(streamWriter, _store);
            streamWriter.Close();
        }

        public IList<SQLConnectionProperties> LoadConnections()
        {
            if (_store.Connections.Count == 0)
            {
                if (!Directory.Exists(appDataPath))
                    Directory.CreateDirectory(appDataPath);

                if (!File.Exists(appDataPath + "\\Connections.txt"))
                    return new List<SQLConnectionProperties>();

                var streamReader = new StreamReader(appDataPath + "\\Connections.txt");
                XmlSerializer serializer = new XmlSerializer(typeof(ConnectionStore));
                _store = serializer.Deserialize(streamReader) as ConnectionStore;
                streamReader.Close();
            }

            return _store.Connections;
        }
    }
}