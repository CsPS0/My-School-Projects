using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class ManageSavesViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty]
        private ObservableCollection<string> _saveFiles;

        public ManageSavesViewModel(Action<ViewModelBase> navigateTo)
        {
            _navigateTo = navigateTo;
            _saveFiles = new ObservableCollection<string>(PetSaveManager.GetAllSaveFiles().Select(Path.GetFileName).Where(f => f != null).Cast<string>());
        }

        [RelayCommand]
        private void DeleteSave(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            PetSaveManager.DeleteSaveFile(fileName);
            SaveFiles.Remove(fileName);
        }

        [RelayCommand]
        private void DeleteAllSaves()
        {
            foreach (var file in SaveFiles.ToList())
            {
                PetSaveManager.DeleteSaveFile(file);
                SaveFiles.Remove(file);
            }
        }

        [RelayCommand]
        private void Back()
        {
            _navigateTo(new SettingsViewModel(_navigateTo));
        }
    }
}
