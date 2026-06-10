using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using RusztikusAdmin.Models;
using RusztikusAdmin.Services;

namespace RusztikusAdmin.ViewModels
{
    public class BookingsViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private List<Booking> _allBookings = new();
        private ObservableCollection<Booking> _bookings = new();
        private Booking? _selectedBooking;
        private string _searchQuery = string.Empty;

        public BookingsViewModel(DataService dataService)
        {
            _dataService = dataService;
            
            SearchCommand = ReactiveCommand.Create(FilterBookings);
            AddBookingCommand = ReactiveCommand.CreateFromTask(AddBooking);
            EditCommand = ReactiveCommand.Create<Booking>(EditBooking);
            DeleteCommand = ReactiveCommand.CreateFromTask<Booking>(DeleteBooking);

            // Load data initially
            LoadDataAsync();
        }

        public ObservableCollection<Booking> Bookings
        {
            get => _bookings;
            set => this.RaiseAndSetIfChanged(ref _bookings, value);
        }

        public Booking? SelectedBooking
        {
            get => _selectedBooking;
            set => this.RaiseAndSetIfChanged(ref _selectedBooking, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set => this.RaiseAndSetIfChanged(ref _searchQuery, value);
        }

        public ReactiveCommand<Unit, Unit> SearchCommand { get; }
        public ReactiveCommand<Unit, Unit> AddBookingCommand { get; }
        public ReactiveCommand<Booking, Unit> EditCommand { get; }
        public ReactiveCommand<Booking, Unit> DeleteCommand { get; }

        public async void LoadDataAsync()
        {
            var bookings = await _dataService.LoadBookingsAsync();
            _allBookings = bookings;
            FilterBookings();
        }

        private void FilterBookings()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Bookings = new ObservableCollection<Booking>(_allBookings);
            }
            else
            {
                var filtered = _allBookings.Where(b => 
                    b.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    b.Phone.Contains(SearchQuery) ||
                    b.Date.Contains(SearchQuery));
                Bookings = new ObservableCollection<Booking>(filtered);
            }
        }

        private async Task AddBooking()
        {
            // In a real app, this would open a dialog
            // For now, we'll add a dummy booking for demonstration
            var newBooking = new Booking
            {
                Id = DateTime.Now.Ticks.ToString(), // Simple ID gen
                Name = "Új Foglalás",
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Time = "12:00",
                Guests = 2,
                TableNumber = 1,
                Status = "Megerősítve"
            };
            
            try
            {
                await _dataService.AddBookingAsync(newBooking);
                LoadDataAsync();
            }
            catch (Exception ex)
            {
                // Handle error, perhaps show message
                Console.WriteLine($"Error adding booking: {ex.Message}");
            }
        }

        private void EditBooking(Booking booking)
        {
            // Edit logic placeholder
        }

        private async Task DeleteBooking(Booking booking)
        {
            if (booking != null)
            {
                try
                {
                    await _dataService.DeleteBookingAsync(booking.Id);
                    LoadDataAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting booking: {ex.Message}");
                }
            }
        }
    }
}