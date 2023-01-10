using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.Repositories;
using WPFApp.ViewModels;

namespace WPFApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;

        //Properties
        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowDonorViewCommand { get; }
        public ICommand ShowPatientViewCommand { get; }
        public ICommand ShowDonorsInfoViewCommand { get; }
        public ICommand ShowPatientsInfoViewCommand { get; }
        public ICommand ShowActivitiesViewCommand { get; }
        public ICommand ShowAppointmentsViewCommand { get; }
        public ICommand ShowRequestViewCommand { get; }
        public ICommand ShowReceiptViewCommand { get; }
        public ICommand ShowStockViewCommand { get; }
        public ICommand ShowStaffViewCommand { get; }
        public ICommand ShowStaffsInfoViewCommand { get; }
        public ICommand ShowProfileViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowDonorViewCommand = new ViewModelCommand(ExecuteShowDonorViewCommand);
            ShowPatientViewCommand = new ViewModelCommand(ExecuteShowPatientViewCommand);
            ShowDonorsInfoViewCommand = new ViewModelCommand(ExecuteShowDonorsInfoViewCommand);
            ShowPatientsInfoViewCommand = new ViewModelCommand(ExecuteShowPatientsInfoViewCommand);
            ShowActivitiesViewCommand = new ViewModelCommand(ExecuteShowActivitiesViewCommand);
            ShowAppointmentsViewCommand = new ViewModelCommand(ExecuteShowAppointmentsViewCommand);
            ShowRequestViewCommand = new ViewModelCommand(ExecuteShowRequestViewCommand);
            ShowReceiptViewCommand = new ViewModelCommand(ExecuteShowReceiptViewCommand);
            ShowStockViewCommand = new ViewModelCommand(ExecuteShowStockViewCommand);
            ShowStaffViewCommand = new ViewModelCommand(ExecuteShowStaffViewCommand);
            ShowStaffsInfoViewCommand = new ViewModelCommand(ExecuteShowStaffsInfoViewCommand);
            ShowProfileViewCommand = new ViewModelCommand(ExecuteShowProfileViewCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }
        private void ExecuteShowDonorViewCommand(object obj)
        {
            CurrentChildView = new DonorViewModel();
            Caption = "Donors";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowPatientViewCommand(object obj)
        {
            CurrentChildView = new PatientViewModel();
            Caption = "Patients";
            Icon = IconChar.BedPulse;
        }
        private void ExecuteShowDonorsInfoViewCommand(object obj)
        {
            CurrentChildView = new DonorsInfoViewModel();
            Caption = "Donor's Information";
            Icon = IconChar.List;
        }
        private void ExecuteShowPatientsInfoViewCommand(object obj)
        {
            CurrentChildView = new PatientsInfoViewModel();
            Caption = "Patient's Information";
            Icon = IconChar.List;
        }
        private void ExecuteShowActivitiesViewCommand(object obj)
        {
            CurrentChildView = new ActivitiesViewModel();
            Caption = "Manage Operations";
            Icon = IconChar.CalendarCheck;
        }
        private void ExecuteShowAppointmentsViewCommand(object obj)
        {
            CurrentChildView = new AppointmentsViewModel();
            Caption = "Manage Appointments";
            Icon = IconChar.PenToSquare;
        }
        private void ExecuteShowRequestViewCommand(object obj)
        {
            CurrentChildView = new RequestViewModel();
            Caption = "Manage Requests";
            Icon = IconChar.HandHoldingMedical;
        }
        private void ExecuteShowReceiptViewCommand(object obj)
        {
            CurrentChildView = new ReceiptViewModel();
            Caption = "Manage Receipts";
            Icon = IconChar.Receipt;
        }
        private void ExecuteShowStockViewCommand(object obj)
        {
            CurrentChildView = new StockViewModel();
            Caption = "Current Stock";
            Icon = IconChar.BoxesStacked;
        }
        private void ExecuteShowStaffViewCommand(object obj)
        {
            CurrentChildView = new StaffViewModel();
            Caption = "View Staff";
            Icon = IconChar.ClipboardUser;
        }
        private void ExecuteShowStaffsInfoViewCommand(object obj)
        {
            CurrentChildView = new StaffsInfoViewModel();
            Caption = "Staff's Information";
            Icon = IconChar.List;
        }
        private void ExecuteShowProfileViewCommand(object obj)
        {
            CurrentChildView = new ProfileViewModel();
            Caption = "Your Profile";
            Icon = IconChar.User;
        }
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Welcome {user.Username}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                //Hide child views.
            }
        }
    }
}
