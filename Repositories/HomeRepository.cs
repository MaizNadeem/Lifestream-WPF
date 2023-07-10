using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPFApp.Models;

namespace WPFApp.Repositories
{
    public class HomeRepository : RepositoryBase
    {
        public IEnumerable<BloodTypeGraphData> GetBloodTypeGraphData(DateTime startDate, DateTime endDate, int intervalDays)
        {
            string query = @"SELECT GraphDate, BloodType, DonationCount
                     FROM dbo.GetBloodTypeGraphData(@StartDate, @EndDate, @IntervalDays)";

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@IntervalDays", intervalDays);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<BloodTypeGraphData> bloodTypeGraphData = new List<BloodTypeGraphData>();

                        while (reader.Read())
                        {
                            BloodTypeGraphData data = new BloodTypeGraphData
                            {
                                GraphDate = reader.GetDateTime(0),
                                BloodType = reader.GetString(1),
                                DonationCount = reader.GetInt32(2)
                            };

                            bloodTypeGraphData.Add(data);
                        }

                        return bloodTypeGraphData;
                    }
                }
            }
        }

    }
}
