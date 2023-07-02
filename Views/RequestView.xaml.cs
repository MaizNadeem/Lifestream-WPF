using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPFApp.Models;
using WPFApp.Repositories;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    public partial class RequestView : UserControl
    {
        private RequestViewModel requestViewModel;
        private ObservableCollection<RequestModel> filteredRequests;

        public RequestView()
        {
            InitializeComponent();
            requestViewModel = new RequestViewModel();
            filteredRequests = new ObservableCollection<RequestModel>(requestViewModel.Requests);
            RequestGrid.ItemsSource = filteredRequests;
        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            FilterRequests();
        }

        private void FilterRequests()
        {
            filteredRequests.Clear();

            string searchQuery = SearchTextBox.Text.ToLower();
            string bloodType = BloodTypeCombo.SelectedItem?.ToString();
            DateTime fromDate = FromDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime toDate = ToDatePicker.SelectedDate ?? DateTime.MaxValue;

            RequestRepository requestRepository = new RequestRepository();
            PatientRepository patientRepository = new PatientRepository();

            // Retrieve requests based on blood type, name, and date range
            var matchingRequests = requestRepository.GetRequestsByBloodTypeAndName(bloodType, searchQuery);

            foreach (RequestModel request in matchingRequests)
            {
                // Get patient name for the request
                PatientModel patient = patientRepository.GetPatientByID(request.PatientID);
                if (patient != null && patient.Name.ToLower().Contains(searchQuery))
                {
                    // Filter requests by date range
                    if (request.PlacementDate >= fromDate && request.PlacementDate <= toDate)
                    {
                        filteredRequests.Add(request);
                    }
                }
            }
        }
    }
}
