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
                command.CommandText = "SELECT [Donor_ID], [Donor_Name], [Donor_DOB], [Donor_Gender], [Donor_BloodType], [Donor_Contact], [Donor_Address], [Donor_Frequency], [Donor_LastDonated] FROM [BloodBank].[dbo].[Donor]";

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds, "Donor");

                if (Donor == null)
                {
                    Donor = new ObservableCollection<DonorModel>();
                }

                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    Donor.Add(new DonorModel()
                    {
                        ID = Convert.ToInt32(reader["Donor_ID"]),
                        Name = reader["Donor_Name"].ToString(),
                        DOB = Convert.ToDateTime(reader["Donor_DOB"]),
                        Gender = reader["Donor_Gender"].ToString(),
                        BloodType = reader["Donor_BloodType"].ToString(),
                        Contact = reader["Donor_Contact"].ToString(),
                        Address = reader["Donor_Address"].ToString(),
                        Frequency = reader["Donor_Frequency"].ToString(),
                        LastDonated = Convert.ToDateTime(reader["Donor_LastDonated"])
                    });
                }
                da.Dispose();
                connection.Close();
            }
            return Donor;
        }
    }
}
