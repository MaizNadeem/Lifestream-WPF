using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using WPFApp.Models;

namespace WPFApp.Repositories
{
    public class RequestRepository : RepositoryBase
    {
        public ObservableCollection<RequestModel> GetRequests()
        {
            ObservableCollection<RequestModel> requests = new ObservableCollection<RequestModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [Request_ID], [Request_BloodType], [Request_Quantity], [Request_PlaceDate], [Request_RequiredDate], [Request_Status], [Patient_ID] FROM [BloodBank].[dbo].[Request]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int requestId = reader.GetInt32(reader.GetOrdinal("Request_ID"));
                        string bloodType = reader.GetString(reader.GetOrdinal("Request_BloodType"));
                        int quantity = reader.GetInt32(reader.GetOrdinal("Request_Quantity"));
                        DateTime placeDate = reader.GetDateTime(reader.GetOrdinal("Request_PlaceDate"));
                        DateTime requiredDate = reader.GetDateTime(reader.GetOrdinal("Request_RequiredDate"));
                        string status = reader.GetString(reader.GetOrdinal("Request_Status"));
                        int patientId = reader.GetInt32(reader.GetOrdinal("Patient_ID"));

                        requests.Add(new RequestModel
                        {
                            RequestID = requestId,
                            BloodType = bloodType,
                            Quantity = quantity,
                            PlaceDate = placeDate,
                            RequiredDate = requiredDate,
                            Status = status,
                            PatientID = patientId
                        });
                    }
                }

                connection.Close();
            }

            return requests;
        }

        public void AddRequest(string bloodType, int quantity, DateTime placeDate, DateTime requiredDate, string status, int patientId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [BloodBank].[dbo].[Request] ([Request_BloodType], [Request_Quantity], [Request_PlaceDate], [Request_RequiredDate], [Request_Status], [Patient_ID]) " +
                                      "VALUES (@BloodType, @Quantity, @PlaceDate, @RequiredDate, @Status, @PatientID)";

                command.Parameters.AddWithValue("@BloodType", bloodType);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@PlaceDate", placeDate);
                command.Parameters.AddWithValue("@RequiredDate", requiredDate);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@PatientID", patientId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateRequest(int requestId, string bloodType, int quantity, DateTime? placeDate, DateTime? requiredDate, string status, int patientId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [BloodBank].[dbo].[Request] SET " +
                                      "[Request_BloodType] = COALESCE(@BloodType, [Request_BloodType]), " +
                                      "[Request_Quantity] = COALESCE(@Quantity, [Request_Quantity]), " +
                                      "[Request_PlaceDate] = COALESCE(@PlaceDate, [Request_PlaceDate]), " +
                                      "[Request_RequiredDate] = COALESCE(@RequiredDate, [Request_RequiredDate]), " +
                                      "[Request_Status] = COALESCE(@Status, [Request_Status]), " +
                                      "[Patient_ID] = COALESCE(@PatientID, [Patient_ID]) " +
                                      "WHERE [Request_ID] = @RequestID";

                command.Parameters.AddWithValue("@RequestID", requestId);
                command.Parameters.AddWithValue("@BloodType", !string.IsNullOrEmpty(bloodType) ? (object)bloodType : DBNull.Value);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@PlaceDate", placeDate.HasValue ? (object)placeDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@RequiredDate", requiredDate.HasValue ? (object)requiredDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Status", !string.IsNullOrEmpty(status) ? (object)status : DBNull.Value);
                command.Parameters.AddWithValue("@PatientID", patientId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteRequest(int requestId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [BloodBank].[dbo].[Request] WHERE [Request_ID] = @RequestID";

                command.Parameters.AddWithValue("@RequestID", requestId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public ObservableCollection<RequestModel> GetRequestsByBloodType(string bloodType)
        {
            ObservableCollection<RequestModel> requests = new ObservableCollection<RequestModel>();

            if (bloodType == "All Blood Types" || bloodType == "--Select Type--" || bloodType == null) {
                RequestRepository req = new RequestRepository();
                requests = req.GetRequests();
                return requests;
            }

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [BloodBank].[dbo].[Request] WHERE [Request_BloodType] = @BloodType";
                command.Parameters.AddWithValue("@BloodType", bloodType);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int requestId = reader.GetInt32(reader.GetOrdinal("Request_ID"));
                        string bbloodType = reader.GetString(reader.GetOrdinal("Request_BloodType"));
                        int quantity = reader.GetInt32(reader.GetOrdinal("Request_Quantity"));
                        DateTime placeDate = reader.GetDateTime(reader.GetOrdinal("Request_PlaceDate"));
                        DateTime requiredDate = reader.GetDateTime(reader.GetOrdinal("Request_RequiredDate"));
                        string status = reader.GetString(reader.GetOrdinal("Request_Status"));
                        int patientId = reader.GetInt32(reader.GetOrdinal("Patient_ID"));

                        requests.Add(new RequestModel
                        {
                            RequestID = requestId,
                            BloodType = bbloodType,
                            Quantity = quantity,
                            PlaceDate = placeDate,
                            RequiredDate = requiredDate,
                            Status = status,
                            PatientID = patientId
                        });
                    }
                }

                connection.Close();
            }

            return requests;
        }

        public ObservableCollection<RequestModel> GetRequestsByPatientName(string name)
        {
            ObservableCollection<RequestModel> requests = new ObservableCollection<RequestModel>();

            if (name == "Search..." || name == "search..." || name == null)
            {
                RequestRepository req = new RequestRepository();
                requests = req.GetRequests();
                return requests;
            }

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT R.* FROM [BloodBank].[dbo].[Request] R INNER JOIN [BloodBank].[dbo].[Patient] P ON R.[Patient_ID] = P.[Patient_ID] WHERE P.[Patient_Name] LIKE @Name";
                command.Parameters.AddWithValue("@Name", "%" + name + "%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int requestId = reader.GetInt32(reader.GetOrdinal("Request_ID"));
                        string bloodType = reader.GetString(reader.GetOrdinal("Request_BloodType"));
                        int quantity = reader.GetInt32(reader.GetOrdinal("Request_Quantity"));
                        DateTime placeDate = reader.GetDateTime(reader.GetOrdinal("Request_PlaceDate"));
                        DateTime requiredDate = reader.GetDateTime(reader.GetOrdinal("Request_RequiredDate"));
                        string status = reader.GetString(reader.GetOrdinal("Request_Status"));
                        int patientId = reader.GetInt32(reader.GetOrdinal("Patient_ID"));

                        requests.Add(new RequestModel
                        {
                            RequestID = requestId,
                            BloodType = bloodType,
                            Quantity = quantity,
                            PlaceDate = placeDate,
                            RequiredDate = requiredDate,
                            Status = status,
                            PatientID = patientId
                        });
                    }
                }

                connection.Close();
            }

            return requests;
        }
    }
}
