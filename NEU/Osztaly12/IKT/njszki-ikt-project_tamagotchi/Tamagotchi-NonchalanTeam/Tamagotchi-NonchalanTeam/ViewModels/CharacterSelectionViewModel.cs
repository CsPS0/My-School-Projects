using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class CharacterSelectionViewModel : ViewModelBase
    {
        private readonly string _petName;
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty]
        private Bitmap? _csongorImage;

        [ObservableProperty]
        private Bitmap? _davidImage;

        public CharacterSelectionViewModel(string petName, Action<ViewModelBase> navigateTo)
        {
            _petName = petName;
            _navigateTo = navigateTo;
            CsongorImage = LoadBitmap("csps-statuses/kalap-feherpolo-rovidnadrag/semleges.png");
            DavidImage = LoadBitmap("lobster-statuses/lobster-semleges.png");
        }

        [RelayCommand]
        private void SelectCsongor()
        {
            var pet = new CsongorPet { Name = _petName };
            _navigateTo(new GameViewModel(pet, _navigateTo));
        }

        [RelayCommand]
        private void SelectDavid()
        {
            var pet = new DavidPet { Name = _petName };
            _navigateTo(new GameViewModel(pet, _navigateTo));
        }

        [RelayCommand]
        private void Back()
        {
            _navigateTo(new NamePetViewModel(_navigateTo));
        }

        private Bitmap LoadBitmap(string fileName)
        {
            string[] assemblyNames = { "Tamagotchi-NonchalanTeam", "Tamagotchi_NonchalanTeam" };
            foreach (var assemblyName in assemblyNames)
            {
                try
                {
                    var uri = new Uri($"avares://{assemblyName}/Images/{fileName}");
                    if (AssetLoader.Exists(uri))
                    {
                        return new Bitmap(AssetLoader.Open(uri));
                    }
                }
                catch { }
            }

            try
            {
                return new Bitmap(AssetLoader.Open(new Uri("avares://Tamagotchi-NonchalanTeam/Assets/avalonia-logo.ico")));
            }
            catch
            {
                return null!;
            }
        }
    }
}