using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WPFApp.ViewModels
{
    public class StockViewModel : ViewModelBase
    {
        private ObservableCollection<StockItem> stockItems;

        public ObservableCollection<StockItem> StockItems
        {
            get { return stockItems; }
            set
            {
                stockItems = value;
                OnPropertyChanged(nameof(StockItems));
            }
        }

        public StockViewModel() 
        {
            GetStockItems("All Bags");
        }

        public void GetStockItems(string bagHealth)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM dbo.BloodStock(@baghealth)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameter to the command
                    command.Parameters.AddWithValue("@baghealth", bagHealth);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        StockItems = new ObservableCollection<StockItem>();

                        while (reader.Read())
                        {
                            StockItems.Add(new StockItem
                            {
                                Stock = (int)reader["Stock"],
                                BagType = reader["Bag_Type"].ToString()
                            });
                        }
                    }
                }
            }
        }
    }

    public class StockItem
    {
        public int Stock { get; set; }
        public string BagType { get; set; }
    }
}
