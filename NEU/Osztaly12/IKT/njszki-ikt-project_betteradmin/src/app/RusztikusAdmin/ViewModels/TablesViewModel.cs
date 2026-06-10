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
    public class TablesViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private ObservableCollection<Table> _tables = new();

        public TablesViewModel(DataService dataService)
        {
            _dataService = dataService;
            AddTableCommand = ReactiveCommand.CreateFromTask(AddTable);
            DeleteTableCommand = ReactiveCommand.CreateFromTask<Table>(DeleteTable);
            LoadDataAsync();
        }

        public ObservableCollection<Table> Tables
        {
            get => _tables;
            set => this.RaiseAndSetIfChanged(ref _tables, value);
        }

        public ReactiveCommand<Unit, Unit> AddTableCommand { get; }
        public ReactiveCommand<Table, Unit> DeleteTableCommand { get; }

        public async void LoadDataAsync()
        {
            try
            {
                var tables = await _dataService.LoadTablesAsync();
                Tables = new ObservableCollection<Table>(tables);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tables: {ex.Message}");
                // Ideally show a notification to the user
            }
        }

        private async Task AddTable()
        {
            var newTable = new Table
            {
                Id = Tables.Count + 1,
                Number = Tables.Count + 1,
                Capacity = 4,
                Location = "Terasz",
                Available = true
            };
            try
            {
                await _dataService.AddTableAsync(newTable);
                LoadDataAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding table: {ex.Message}");
            }
        }

        private async Task DeleteTable(Table table)
        {
            if (table != null)
            {
                try
                {
                    await _dataService.DeleteTableAsync(table.Id);
                    LoadDataAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting table: {ex.Message}");
                }
            }
        }
    }
}