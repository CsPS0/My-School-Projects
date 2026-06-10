using System.Linq;
using ReactiveUI;
using RusztikusAdmin.Services;

namespace RusztikusAdmin.ViewModels
{
    public class StatsViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private int _todayBookingsCount;
        private int _todayGuestsCount;
        private int _menuItemsCount;
        private int _availableTablesCount;

        public StatsViewModel(DataService dataService)
        {
            _dataService = dataService;
            LoadStatsAsync();
        }

        public int TodayBookingsCount
        {
            get => _todayBookingsCount;
            set => this.RaiseAndSetIfChanged(ref _todayBookingsCount, value);
        }

        public int TodayGuestsCount
        {
            get => _todayGuestsCount;
            set => this.RaiseAndSetIfChanged(ref _todayGuestsCount, value);
        }

        public int MenuItemsCount
        {
            get => _menuItemsCount;
            set => this.RaiseAndSetIfChanged(ref _menuItemsCount, value);
        }

        public int AvailableTablesCount
        {
            get => _availableTablesCount;
            set => this.RaiseAndSetIfChanged(ref _availableTablesCount, value);
        }

        public async void LoadStatsAsync()
        {
            try
            {
                var bookings = await _dataService.LoadBookingsAsync();
                var menu = await _dataService.LoadMenuAsync();
                var tables = await _dataService.LoadTablesAsync();

                string today = System.DateTime.Now.ToString("yyyy-MM-dd");
                var todaysBookings = bookings.Where(b => b.Date == today).ToList();

                TodayBookingsCount = todaysBookings.Count;
                TodayGuestsCount = todaysBookings.Sum(b => b.Guests);
                MenuItemsCount = menu.Count;
                AvailableTablesCount = tables.Count(t => t.Available);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error loading stats: {ex.Message}");
            }
        }
    }
}