using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WPFApp.Repositories
{
    public class RepositoryBase
    {
        private readonly string _connectionString = "Server=tcp:database-server-bds.database.windows.net,1433;Initial Catalog=BloodBank;Persist Security Info=False;User ID=maiznadeem;Password=SnC2ApayPpi48b7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public RepositoryBase()
        {

        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
