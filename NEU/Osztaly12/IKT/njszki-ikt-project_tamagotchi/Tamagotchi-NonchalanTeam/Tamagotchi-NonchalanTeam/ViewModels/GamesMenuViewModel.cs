using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class GamesMenuViewModel : ViewModelBase
    {
        private readonly Pet _pet;
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty] private string _statusMessage = "Select a game to play!";

        public GamesMenuViewModel(Pet pet, Action<ViewModelBase> navigateTo)
        {
            _pet = pet;
            _navigateTo = navigateTo;
        }

        [RelayCommand]
        private void StartSnake()
        {
            _navigateTo(new SnakeViewModel(_pet, _navigateTo));
        }

        [RelayCommand]
        private void StartTicTacToe()
        {
            _navigateTo(new TicTacToeViewModel(_pet, _navigateTo));
        }

        [RelayCommand]
        private void Back()
        {
            _navigateTo(new GameViewModel(_pet, _navigateTo));
        }
    }
}

