using System;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.Repositories;
using WPFApp.Views;

namespace WPFApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        // Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isLoggingIn;

        private IUserRepository userRepository;

        // Properties
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set
            {
                _isLoggingIn = value;
                OnPropertyChanged(nameof(IsLoggingIn));
                OnPropertyChanged(nameof(LoginButtonText));
                ((ViewModelCommand)LoginCommand).RaiseCanExecuteChanged(); // Notify command of state change
            }
        }

        public string LoginButtonText => IsLoggingIn ? "Logging in..." : "LOG IN";

        // Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        // Constructor
        public LoginViewModel()
        {
            Username = "johndoe"; // Set your default username here
            Password = ConvertToSecureString("password1");
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(async (param) => await ExecuteLoginCommand(param), CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            var securePassword = new SecureString();
            foreach (char c in password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            return securePassword;
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            return !IsLoggingIn && !string.IsNullOrWhiteSpace(Username) && Username.Length >= 3 &&
                   Password != null && Password.Length >= 3;
        }

        private async Task ExecuteLoginCommand(object obj)
        {
            IsLoggingIn = true;
            ErrorMessage = string.Empty;

            try
            {
                var isValidUser = await Task.Run(() =>
                    userRepository.AuthenticateUser(new NetworkCredential(Username, Password))
                );

                if (isValidUser)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);

                        var mainView = new MainView();
                        mainView.Show();
                        if (Application.Current.MainWindow != null)
                        {
                            Application.Current.MainWindow.Close();
                        }
                        Application.Current.MainWindow = mainView;
                    });
                }
                else
                {
                    ErrorMessage = "* Invalid username or password";
                }
            }
            finally
            {
                IsLoggingIn = false;
            }
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
