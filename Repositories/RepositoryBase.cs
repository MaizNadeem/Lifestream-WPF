using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace WPFApp.Repositories
{
    public class RepositoryBase
    {
        private readonly string _connectionString;
        
        public RepositoryBase()
        {
            _connectionString = "AZURE_CONNECTION_STRING";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
