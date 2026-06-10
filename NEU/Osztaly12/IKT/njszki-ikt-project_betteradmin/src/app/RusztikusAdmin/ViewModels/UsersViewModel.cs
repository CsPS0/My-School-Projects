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
    public class UsersViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private List<User> _allUsers = new();
        private ObservableCollection<User> _users = new();
        private string _searchQuery = string.Empty;

        public UsersViewModel(DataService dataService)
        {
            _dataService = dataService;
            
            SearchCommand = ReactiveCommand.Create(FilterUsers);
            DeleteCommand = ReactiveCommand.CreateFromTask<User>(DeleteUser);

            LoadDataAsync();
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set => this.RaiseAndSetIfChanged(ref _users, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set => this.RaiseAndSetIfChanged(ref _searchQuery, value);
        }

        public ReactiveCommand<Unit, Unit> SearchCommand { get; }
        public ReactiveCommand<User, Unit> DeleteCommand { get; }

        public async void LoadDataAsync()
        {
            try
            {
                var users = await _dataService.LoadUsersAsync();
                _allUsers = users;
                FilterUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
            }
        }

        private void FilterUsers()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Users = new ObservableCollection<User>(_allUsers);
            }
            else
            {
                var filtered = _allUsers.Where(u => 
                    u.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    u.FullName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
                Users = new ObservableCollection<User>(filtered);
            }
        }

        private async Task DeleteUser(User user)
        {
            if (user != null)
            {
                if (user.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    // Protection against deleting the admin user
                    Console.WriteLine("Cannot delete admin user.");
                    return;
                }

                try
                {
                    await _dataService.DeleteUserAsync(user.Id);
                    LoadDataAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting user: {ex.Message}");
                }
            }
        }
    }
}