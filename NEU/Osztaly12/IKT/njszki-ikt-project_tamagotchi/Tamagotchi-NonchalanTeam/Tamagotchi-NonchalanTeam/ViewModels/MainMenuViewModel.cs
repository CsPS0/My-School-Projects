using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class MainMenuViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty]
        private string _statusMessage = "Welcome to Tamagotchi!";

        public MainMenuViewModel(Action<ViewModelBase> navigateTo)
        {
            _navigateTo = navigateTo;
        }

        [RelayCommand]
        private void NewGame()
        {
            _navigateTo(new NamePetViewModel(_navigateTo));
        }

        [RelayCommand]
        private void LoadGame()
        {
            _navigateTo(new LoadGameViewModel(_navigateTo));
        }

        [RelayCommand]
        private void OpenSettings()
        {
            _navigateTo(new SettingsViewModel(_navigateTo));
        }

        [RelayCommand]
        private void OpenScoreboard()
        {
            _navigateTo(new ScoreboardViewModel(_navigateTo));
        }

        [RelayCommand]
        private void Exit()
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
