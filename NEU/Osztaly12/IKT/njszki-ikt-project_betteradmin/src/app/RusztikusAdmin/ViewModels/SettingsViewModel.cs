using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using RusztikusAdmin.Services;

namespace RusztikusAdmin.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly SettingsService _settingsService;

        private string _restaurantName = "Rusztikus Étterem";
        private string _address = "1052 Budapest, Petőfi Sándor utca 5.";
        private string _phone = "+36 1 234 5678";
        private string _email = "info@rusztikusetterem.hu";
        private string _weekdaysHours = "11:00 - 23:00";
        private string _weekendsHours = "12:00 - 24:00";
        private string _statusMessage = string.Empty;

        public SettingsViewModel()
        {
            _settingsService = new SettingsService();
            // Load settings asynchronously
            System.Threading.Tasks.Task.Run(LoadSettings);

            SaveCommand = ReactiveCommand.CreateFromTask(SaveSettings);
            CancelCommand = ReactiveCommand.CreateFromTask(LoadSettings);
        }

        public string RestaurantName
        {
            get => _restaurantName;
            set => this.RaiseAndSetIfChanged(ref _restaurantName, value);
        }

        public string Address
        {
            get => _address;
            set => this.RaiseAndSetIfChanged(ref _address, value);
        }

        public string Phone
        {
            get => _phone;
            set => this.RaiseAndSetIfChanged(ref _phone, value);
        }

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string WeekdaysHours
        {
            get => _weekdaysHours;
            set => this.RaiseAndSetIfChanged(ref _weekdaysHours, value);
        }

        public string WeekendsHours
        {
            get => _weekendsHours;
            set => this.RaiseAndSetIfChanged(ref _weekendsHours, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => this.RaiseAndSetIfChanged(ref _statusMessage, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private async Task LoadSettings()
        {
            var settings = await _settingsService.LoadSettingsAsync();
            RestaurantName = settings.RestaurantName;
            Address = settings.Address;
            Phone = settings.Phone;
            Email = settings.Email;
            WeekdaysHours = settings.OpeningHours.Weekdays;
            WeekendsHours = settings.OpeningHours.Weekends;
            StatusMessage = string.Empty;
        }

        private async Task SaveSettings()
        {
            var settings = new Models.RestaurantSettings
            {
                RestaurantName = RestaurantName,
                Address = Address,
                Phone = Phone,
                Email = Email,
                OpeningHours = new Models.OpeningHours
                {
                    Weekdays = WeekdaysHours,
                    Weekends = WeekendsHours
                }
            };

            try 
            {
                await _settingsService.SaveSettingsAsync(settings);
                StatusMessage = "Beállítások sikeresen mentve!";
                await Task.Delay(3000);
                StatusMessage = string.Empty;
            }
            catch
            {
                StatusMessage = "Hiba történt a mentés során!";
            }
        }
    }
}