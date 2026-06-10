using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class NamePetViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartCommand))]
        private string _petName = "";

        public NamePetViewModel(Action<ViewModelBase> navigateTo)
        {
            _navigateTo = navigateTo;
        }

        [RelayCommand(CanExecute = nameof(CanStart))]
        private void Start()
        {
            _navigateTo(new CharacterSelectionViewModel(PetName, _navigateTo));
        }

        private bool CanStart => !string.IsNullOrWhiteSpace(PetName);

        [RelayCommand]
        private void Back()
        {
            _navigateTo(new MainMenuViewModel(_navigateTo));
        }
    }
}
