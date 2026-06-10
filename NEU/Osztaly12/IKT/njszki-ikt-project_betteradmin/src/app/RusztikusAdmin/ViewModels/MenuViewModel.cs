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
    public class MenuViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private ObservableCollection<MenuItem> _menuItems = new();
        private MenuItem? _selectedMenuItem;

        public MenuViewModel(DataService dataService)
        {
            _dataService = dataService;
            NewMenuItemCommand = ReactiveCommand.CreateFromTask(AddMenuItem);
            DeleteMenuItemCommand = ReactiveCommand.CreateFromTask<MenuItem>(DeleteMenuItem);
            LoadDataAsync();
        }

        public ObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set => this.RaiseAndSetIfChanged(ref _menuItems, value);
        }

        public MenuItem? SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => this.RaiseAndSetIfChanged(ref _selectedMenuItem, value);
        }

        public ReactiveCommand<Unit, Unit> NewMenuItemCommand { get; }
        public ReactiveCommand<MenuItem, Unit> DeleteMenuItemCommand { get; }

        public async void LoadDataAsync()
        {
            var items = await _dataService.LoadMenuAsync();
            MenuItems = new ObservableCollection<MenuItem>(items);
        }

        private async Task AddMenuItem()
        {
            var newItem = new MenuItem
            {
                Id = System.DateTime.Now.Ticks,
                Name = "Új Étel",
                Price = 1000,
                Category = "Egyéb",
                Available = true
            };
            try
            {
                await _dataService.AddMenuItemAsync(newItem);
                LoadDataAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding menu item: {ex.Message}");
            }
        }

        private async Task DeleteMenuItem(MenuItem item)
        {
            if (item != null)
            {
                try
                {
                    await _dataService.DeleteMenuItemAsync(item.Id);
                    LoadDataAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting menu item: {ex.Message}");
                }
            }
        }
    }
}