﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.SqlServer
{
    internal class ConnectionStringBuilder
    {
        private string connectionStringFormat = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";

        public string Build(string server, string initialCatalog, string username, string password)
        {

            return "Data Source=.\\sqlexpress;Initial Catalog=Flexilineweb;Integrated Security=SSPI;";


            return string.Format(connectionStringFormat, server, initialCatalog, username, password);
        }
    }
}
