using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using WPFApp.Models;

namespace WPFApp.Repositories
{
    public class AppointmentRepository : RepositoryBase
    {
        public ObservableCollection<AppointmentModel> GetAppointments()
        {
            ObservableCollection<AppointmentModel> appointments = new ObservableCollection<AppointmentModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [id], [appo_name], [appo_placedate], [appo_datetime], [appo_status], [donor_id] FROM [BloodBank].[dbo].[Appointment]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("appo_name"));
                        DateTime placementDate = reader.GetDateTime(reader.GetOrdinal("appo_placedate"));
                        DateTime appointmentDate = reader.GetDateTime(reader.GetOrdinal("appo_datetime"));
                        string status = reader.GetString(reader.GetOrdinal("appo_status"));
                        int donorId = reader.GetInt32(reader.GetOrdinal("donor_id"));

                        appointments.Add(new AppointmentModel
                        {
                            ID = id,
                            Name = name,
                            PlacementDate = placementDate,
                            AppointmentDate = appointmentDate,
                            Status = status,
                            DonorID = donorId
                        });
                    }
                }

                connection.Close();
            }

            return appointments;
        }

        public void AddAppointment(string name, DateTime placementDate, DateTime appointmentDate, string status, int donorId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [BloodBank].[dbo].[Appointment] ([appo_name], [appo_placedate], [appo_datetime], [appo_status], [donor_id]) " +
                                      "VALUES (@Name, @PlacementDate, @AppointmentDate, @Status, @DonorID)";

                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@PlacementDate", placementDate);
                command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@DonorID", donorId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateAppointment(int appointmentId, string name, DateTime? placementDate, DateTime? appointmentDate, string status, int donorId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [BloodBank].[dbo].[Appointment] SET " +
                                      "[appo_name] = COALESCE(@Name, [appo_name]), " +
                                      "[appo_placedate] = COALESCE(@PlacementDate, [appo_placedate]), " +
                                      "[appo_datetime] = COALESCE(@AppointmentDate, [appo_datetime]), " +
                                      "[appo_status] = COALESCE(@Status, [appo_status]), " +
                                      "[donor_id] = COALESCE(@DonorID, [donor_id]) " +
                                      "WHERE [id] = @AppointmentID";

                command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                command.Parameters.AddWithValue("@Name", !string.IsNullOrEmpty(name) ? (object)name : DBNull.Value);
                command.Parameters.AddWithValue("@PlacementDate", placementDate.HasValue ? (object)placementDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@AppointmentDate", appointmentDate.HasValue ? (object)appointmentDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Status", !string.IsNullOrEmpty(status) ? (object)status : DBNull.Value);
                command.Parameters.AddWithValue("@DonorID", donorId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteAppointment(int appointmentId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [BloodBank].[dbo].[Appointment] WHERE [id] = @AppointmentID";

                command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
