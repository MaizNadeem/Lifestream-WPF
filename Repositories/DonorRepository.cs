using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;
using WPFApp.Repositories;

namespace WPFApp.Repositories
{
    public class DonorRepository : RepositoryBase
    {
        public ObservableCollection<DonorModel> Donor;

        public ObservableCollection<DonorModel> DonorGridBind()
        {

            Donor = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [Donor]";

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds, "Donor");

                if (Donor == null ) 
                {
                    Donor = new ObservableCollection<DonorModel>();
                }

                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    Donor.Add(new DonorModel()
                    {
                        Id = Convert.ToInt32(reader[0].ToString()),
                        Name = reader[1].ToString(),
                        DOB = Convert.ToDateTime(reader[2].ToString()),
                        Gender = reader[3].ToString(),
                        BloodType = reader[4].ToString(),
                    });
                }
                da.Dispose();
                connection.Close();
            }
            return Donor;
        }
    }
}