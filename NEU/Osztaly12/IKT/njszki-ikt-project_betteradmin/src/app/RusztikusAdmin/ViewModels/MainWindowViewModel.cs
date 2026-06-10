using System;
using System.Reactive;
using ReactiveUI;
using RusztikusAdmin.Services;
using RusztikusAdmin.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace RusztikusAdmin.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        private readonly DataService _dataService;
        private string _statusMessage = "Készen áll";
        private string _currentUser = "Admin Felhasználó";
        private string _adminName = "Admin";

        public MainWindowViewModel()
        {
            _dataService = new DataService();
            
            // Set user info from AuthService
            if (AuthService.CurrentUser != null)
            {
                CurrentUser = AuthService.CurrentUser.FullName;
                AdminName = AuthService.CurrentUser.Username;
            }

            ShowBookingsCommand = ReactiveCommand.Create(() => { CurrentView = new BookingsViewModel(_dataService); });
            ShowMenuCommand = ReactiveCommand.Create(() => { CurrentView = new MenuViewModel(_dataService); });
            ShowTablesCommand = ReactiveCommand.Create(() => { CurrentView = new TablesViewModel(_dataService); });
            ShowUsersCommand = ReactiveCommand.Create(() => { CurrentView = new UsersViewModel(_dataService); });
            ShowStatsCommand = ReactiveCommand.Create(() => { CurrentView = new StatsViewModel(_dataService); });
            ShowSettingsCommand = ReactiveCommand.Create(() => { CurrentView = new SettingsViewModel(); });
            
            LogoutCommand = ReactiveCommand.Create(Logout);
            
            ExitCommand = ReactiveCommand.Create(() => Environment.Exit(0));
            SaveCommand = ReactiveCommand.Create(() => { StatusMessage = "Adatok mentve."; });
            RefreshCommand = ReactiveCommand.Create(() => 
            {
                // Re-navigate to refresh
                if (CurrentView is BookingsViewModel) ShowBookingsCommand.Execute().Subscribe();
                else if (CurrentView is MenuViewModel) ShowMenuCommand.Execute().Subscribe();
                else if (CurrentView is TablesViewModel) ShowTablesCommand.Execute().Subscribe();
                else if (CurrentView is UsersViewModel) ShowUsersCommand.Execute().Subscribe();
                else if (CurrentView is StatsViewModel) ShowStatsCommand.Execute().Subscribe();
                else if (CurrentView is SettingsViewModel) ShowSettingsCommand.Execute().Subscribe();
                
                StatusMessage = "Frissítve.";
            });

            // Default
            _currentView = new BookingsViewModel(_dataService);
        }

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => this.RaiseAndSetIfChanged(ref _statusMessage, value);
        }

        public string CurrentDate => DateTime.Now.ToString("yyyy. MM. dd.");

        public string CurrentUser
        {
            get => _currentUser;
            set => this.RaiseAndSetIfChanged(ref _currentUser, value);
        }

        public string AdminName
        {
            get => _adminName;
            set => this.RaiseAndSetIfChanged(ref _adminName, value);
        }

        // Navigation Commands
        public ReactiveCommand<Unit, Unit> ShowBookingsCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowMenuCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowTablesCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowUsersCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowStatsCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowSettingsCommand { get; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

        // Menu Bar Commands
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        
        // Placeholder commands for menu items binding
        public ReactiveCommand<Unit, Unit> NewBookingCommand => ReactiveCommand.Create(() => { 
            CurrentView = new BookingsViewModel(_dataService); 
            StatusMessage = "Új foglalás hozzáadása a listanézeten keresztül.";
        });
        
        public ReactiveCommand<Unit, Unit> NewMenuItemCommand => ReactiveCommand.Create(() => {
            CurrentView = new MenuViewModel(_dataService);
            StatusMessage = "Új menüelem hozzáadása a listanézeten keresztül.";
        });

        private void Logout()
        {
            var authService = new AuthService();
            authService.Logout();

            var loginWindow = new LoginWindow();
            loginWindow.Show();

            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = desktop.MainWindow;
                desktop.MainWindow = loginWindow;
                mainWindow?.Close();
            }
        }
    }
}