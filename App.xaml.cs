using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFApp.Views;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {

        }

        private LoginView loginView;
        private MainView mainView;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create the LoginView and subscribe to its visibility changed event
            loginView = new LoginView();
            loginView.IsVisibleChanged += LoginView_IsVisibleChanged;
            loginView.Show();
        }
        
        private async void LoginView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Check if the LoginView is hidden and the MainView is not visible
            if (!loginView.IsVisible && (mainView == null || !mainView.IsVisible))
            {
                // Show the MainView on the next UI message loop
                await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    mainView = new MainView();
                    mainView.Show();

                    // Subscribe to the MainView's visibility changed event
                    mainView.IsVisibleChanged += MainView_IsVisibleChanged;

                    // Close the LoginView
                    loginView.Close();
                }));
            }
        }

        private async void MainView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Check if the MainView is not visible and the LoginView is hidden
            if (!mainView.IsVisible && (loginView == null || !loginView.IsVisible))
            {
                // Show the LoginView on the next UI message loop
                await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    loginView = new LoginView();
                    loginView.Show();

                    // Subscribe to the LoginView's visibility changed event
                    loginView.IsVisibleChanged += LoginView_IsVisibleChanged;

                    // Close the MainView
                    mainView.Close();
                }));
            }
        }
    }
}
