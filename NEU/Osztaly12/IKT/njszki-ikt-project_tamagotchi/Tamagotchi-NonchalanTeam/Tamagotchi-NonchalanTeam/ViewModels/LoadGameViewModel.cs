using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class LoadGameViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty]
        private ObservableCollection<string> _saveFiles;

        public LoadGameViewModel(Action<ViewModelBase> navigateTo)
        {
            _navigateTo = navigateTo;
            _saveFiles = new ObservableCollection<string>(PetSaveManager.GetAllSaveFiles().Select(Path.GetFileName).Where(f => f != null).Cast<string>());
        }

        [RelayCommand]
        private void LoadSave(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            try
            {
                var pet = PetSaveManager.LoadPet(fileName);
                _navigateTo(new GameViewModel(pet, _navigateTo));
            }
            catch (Exception)
            {
            }
        }

        [RelayCommand]
        private void Back()
        {
            _navigateTo(new MainMenuViewModel(_navigateTo));
        }
    }
}
