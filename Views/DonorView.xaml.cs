using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WPFApp.Models;
using WPFApp.Repositories;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    public partial class DonorView : UserControl
    {
        DonorViewModel donorViewModel = new DonorViewModel();

        public DonorView()
        {
            InitializeComponent();
            DonorGrid.ItemsSource = donorViewModel.Donor;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BloodTypeCombo.Text != "All Blood Types")
                DonorGrid.ItemsSource = donorViewModel.Donor.Where(s => s.BloodType == BloodTypeCombo.Text).ToList();
            else
                DonorGrid.ItemsSource = donorViewModel.Donor;

            Console.WriteLine("Data:\n");
            Console.WriteLine(donorViewModel.Donor[0].Name);
        }

        private void BloodTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
