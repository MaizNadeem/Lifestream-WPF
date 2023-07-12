using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using WPFApp.Models;
using WPFApp.Repositories;

namespace WPFApp.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private HomeRepository homeRepository;

        public HomeViewModel()
        {
            homeRepository = new HomeRepository();

            SeriesCollection = new SeriesCollection();
            Labels = new List<string>();

            DateTime startDate = new DateTime(2023, 01, 01);
            DateTime endDate = new DateTime(2023, 12, 31);
            int interval = 30;

            // Call the method in HomeRepository to get the blood type graph data
            List<BloodTypeGraphData> bloodTypeGraphData = homeRepository.GetBloodTypeGraphData(startDate, endDate, interval).ToList();

            // Extract the distinct blood types from the graph data
            List<string> bloodTypes = bloodTypeGraphData.Select(d => d.BloodType).Distinct().ToList();

            // Create LineSeries for each blood type
            foreach (string bloodType in bloodTypes)
            {
                List<double> donationCounts = bloodTypeGraphData
                    .Where(d => d.BloodType == bloodType)
                    .OrderBy(d => d.GraphDate)
                    .Select(d => (double)d.DonationCount)
                    .ToList();

                SeriesCollection.Add(new LineSeries
                {
                    Title = bloodType,
                    Values = new ChartValues<double>(donationCounts),
                    PointGeometrySize = 5,
                    LineSmoothness = 0.5,
                });
            }

            // Get the distinct graph dates
            List<DateTime> graphDates = bloodTypeGraphData.Select(d => d.GraphDate.Date).Distinct().ToList();

            // Create labels for each graph date
            Labels = graphDates.Select(date => date.ToString("dd/MM/yyyy")).ToList();

            YFormatter = value => value.ToString("N0");
        }

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
