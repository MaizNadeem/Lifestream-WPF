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
    public partial class PatientView : UserControl
    {
        PatientViewModel patientViewModel = new PatientViewModel();

        public PatientView()
        {
            InitializeComponent();
            PatientGrid.ItemsSource = patientViewModel.Patient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BloodTypeCombo.Text != "All Blood Types")
                PatientGrid.ItemsSource = patientViewModel.Patient.Where(s => s.BloodType == BloodTypeCombo.Text).ToList();
            else
                PatientGrid.ItemsSource = patientViewModel.Patient;
        }

        private void BloodTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
