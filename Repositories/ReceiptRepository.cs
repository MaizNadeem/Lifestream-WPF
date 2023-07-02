using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using WPFApp.Models;
using System.Linq;
using System.Collections.Generic;

namespace WPFApp.Repositories
{
    public class ReceiptRepository : RepositoryBase
    {
        public ObservableCollection<ReceiptModel> GetReceipts()
        {
            ObservableCollection<ReceiptModel> receipts = new ObservableCollection<ReceiptModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [Receipt_ID], [Receipt_DateTime], [Receipt_BloodType], [Receipt_Quantity], [Receipt_BagHealth], [Receipt_Process], [Staff_ID], [Request_ID], [Appo_ID] FROM [BloodBank].[dbo].[Receipt]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int receiptId = reader.GetInt32(reader.GetOrdinal("Receipt_ID"));
                        DateTime receiptDate = reader.GetDateTime(reader.GetOrdinal("Receipt_DateTime"));
                        string bloodType = reader.GetString(reader.GetOrdinal("Receipt_BloodType"));
                        int quantity = reader.GetInt32(reader.GetOrdinal("Receipt_Quantity"));
                        string bagHealth = reader.GetString(reader.GetOrdinal("Receipt_BagHealth"));
                        string process = reader.GetString(reader.GetOrdinal("Receipt_Process"));
                        int? staffId = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("Staff_ID")))
                        {
                            staffId = reader.GetInt32(reader.GetOrdinal("Staff_ID"));
                        }

                        int? requestId = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("Request_ID")))
                        {
                            requestId = reader.GetInt32(reader.GetOrdinal("Request_ID"));
                        }

                        int? appointmentId = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("Appo_ID")))
                        {
                            appointmentId = reader.GetInt32(reader.GetOrdinal("Appo_ID"));
                        }


                        receipts.Add(new ReceiptModel
                        {
                            ReceiptID = receiptId,
                            ReceiptDate = receiptDate,
                            BloodType = bloodType,
                            Quantity = quantity,
                            BagHealth = bagHealth,
                            Process = process,
                            StaffID = staffId,
                            RequestID = requestId,
                            AppointmentID = appointmentId
                        });
                    }
                }

                connection.Close();
            }

            return receipts;
        }

        public void AddReceipt(DateTime receiptDate, string bloodType, int quantity, string bagHealth, string process, int staffId, int requestId, int appointmentId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [BloodBank].[dbo].[Receipt] ([Receipt_DateTime], [Receipt_BloodType], [Receipt_Quantity], [Receipt_BagHealth], [Receipt_Process], [Staff_ID], [Request_ID], [Appo_ID]) " +
                                      "VALUES (@ReceiptDate, @BloodType, @Quantity, @BagHealth, @Process, @StaffID, @RequestID, @AppointmentID)";

                command.Parameters.AddWithValue("@ReceiptDate", receiptDate);
                command.Parameters.AddWithValue("@BloodType", bloodType);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@BagHealth", bagHealth);
                command.Parameters.AddWithValue("@Process", process);
                command.Parameters.AddWithValue("@StaffID", staffId);
                command.Parameters.AddWithValue("@RequestID", requestId);
                command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateReceipt(int receiptId, DateTime? receiptDate, string bloodType, int quantity, string bagHealth, string process, int staffId, int? requestId, int? appointmentId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [BloodBank].[dbo].[Receipt] SET " +
                                      "[Receipt_DateTime] = COALESCE(@ReceiptDate, [Receipt_Date]), " +
                                      "[Receipt_BloodType] = @BloodType, " +
                                      "[Receipt_Quantity] = @Quantity, " +
                                      "[Receipt_BagHealth] = @BagHealth, " +
                                      "[Receipt_Process] = @Process, " +
                                      "[Staff_ID] = @StaffID, " +
                                      "[Request_ID] = COALESCE(@RequestID, [Request_ID]), " +
                                      "[Appo_ID] = COALESCE(@AppointmentID, [Appointment_ID]) " +
                                      "WHERE [Receipt_ID] = @ReceiptID";

                command.Parameters.AddWithValue("@ReceiptID", receiptId);
                command.Parameters.AddWithValue("@ReceiptDate", (object)receiptDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@BloodType", bloodType);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@BagHealth", bagHealth);
                command.Parameters.AddWithValue("@Process", process);
                command.Parameters.AddWithValue("@StaffID", staffId);
                command.Parameters.AddWithValue("@RequestID", (object)requestId ?? DBNull.Value);
                command.Parameters.AddWithValue("@AppointmentID", (object)appointmentId ?? DBNull.Value);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteReceipt(int receiptId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [BloodBank].[dbo].[Receipt] WHERE [Receipt_ID] = @ReceiptID";
                command.Parameters.AddWithValue("@ReceiptID", receiptId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<ReceiptModel> GetReceiptsByBloodType(string bloodType)
        {
            var receipts = GetReceipts();

            if (bloodType == "--Select Type--" || bloodType == "All Blood Types" || bloodType == null)
            {
                return receipts.ToList();
            }

            // Filter the receipts based on the specified blood type
            var matchingReceipts = receipts.Where(r => r.BloodType == bloodType).ToList();

            return matchingReceipts;
        }
    }
}
