using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using System;
using System.IO;
using System.Linq;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class GameViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _navigateTo;
        private DispatcherTimer? _gameTimer;
        private static readonly Random _random = new();
        private int _virusTimerCounter = 0;
        private int _virusPopCount = 0;
        private DateTime _virusCooldownEndTime = DateTime.MinValue;

        [ObservableProperty]
        private Pet _activePet;

        [ObservableProperty]
        private string _statusMessage = "";

        [ObservableProperty]
        private bool _isVirusVisible = false;

        [ObservableProperty]
        private double _virusX;

        [ObservableProperty]
        private double _virusY;

        [ObservableProperty]
        private Bitmap? _virusImage;

        public Bitmap? ActivePetImage 
        {
            get
            {
                string expression = "semleges";

                if (!ActivePet.IsAlive)
                {
                    expression = "meghalt";
                }
                else if (ActivePet.IsSick)
                {
                    expression = "beteg";
                }
                else if (ActivePet.Hunger < 10 && ActivePet.Hunger < ActivePet.Happiness)
                {
                    expression = "ehes";
                }
                else if (ActivePet.Hunger > 80 && ActivePet.Happiness > 80)
                {
                    expression = "boldog";
                }
                else if (ActivePet.Happiness < 20)
                {
                    expression = "szomoru";
                }

                if (ActivePet.PetType == "Csongor")
                {
                    if (ActivePet.IsTutorialActive)
                    {
                        string tutorialImage = expression;
                        if (ActivePet.TutorialStep == 0)
                        {
                            bool hasHat = ActivePet.OwnedClothes.Any(id => id.Contains("kalap") || id.Contains("maszk"));
                            bool hasShirt = ActivePet.OwnedClothes.Any(id => id.Contains("polo"));
                            bool hasPants = ActivePet.OwnedClothes.Any(id => id.Contains("nadrag") || id.Contains("farmer"));

                            if (!hasShirt && !hasPants) tutorialImage = "1fazis-fazik";
                            else if (!hasHat) tutorialImage = "2fazis-nokalap";
                            else tutorialImage = "3fazis-vegreszep";
                        }
                        return LoadBitmap($"csps-statuses/meztelen-tutorial/{tutorialImage}");
                    }
                    
                    string hat = ActivePet.CurrentHat ?? "";
                    string shirt = ActivePet.CurrentShirt ?? "";
                    string pants = ActivePet.CurrentPants ?? "";

                    if (hat == "buvarmaszk" && pants == "rovidnadrag")
                    {
                        hat = "buvarnaszk";
                    }

                    string folderName = $"{hat}-{shirt}-{pants}";
                    return LoadBitmap($"csps-statuses/{folderName}/{expression}");
                    }

                    return LoadBitmap($"{ActivePet.PetType.ToLower()}-{expression}");
                    }
                    }
        public GameViewModel(Pet pet, Action<ViewModelBase> navigateTo)
        {
            _activePet = pet;
            _navigateTo = navigateTo;
            
            VirusImage = LoadBitmap("virus");
            UpdateStatusMessage();

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _gameTimer.Tick += (s, e) => UpdateGame();
            _gameTimer.Start();
        }

        private void UpdateStatusMessage()
        {
            if (!ActivePet.IsAlive)
            {
                if (ActivePet.DiedWithPride)
                {
                    StatusMessage = $"{ActivePet.Name} survived for 5 minutes and passed away with pride!";
                }
                else
                {
                    StatusMessage = $"{ActivePet.Name} has passed away...";
                }
                return;
            }

            if (ActivePet.IsSick)
            {
                StatusMessage = $"{ActivePet.Name} is sick! You need to work harder! Heal them quickly!";
                return;
            }

            if (ActivePet.IsTutorialActive && ActivePet.TutorialStep == 0 && ActivePet is CsongorPet)
            {
                StatusMessage = "Your character looks really bad when it is naked. Go to the shop and give them clothes!";
            }
            else
            {
                StatusMessage = $"Take care of {ActivePet.Name}!";
            }
        }

        [RelayCommand]
        private void FeedPet()
        {
            if (ActivePet.FoodStock > 0)
            {
                ActivePet.FoodStock--;
                ActivePet.Feed();
                UpdateStatusMessage();
            }
            else
            {
                StatusMessage = "Collect more feed points in Snake game!";
            }
        }

        [RelayCommand]
        private void HealPet()
        {
            ActivePet.Heal();
            UpdateStatusMessage();
        }

        [RelayCommand]
        private void PopVirus()
        {
            IsVirusVisible = false;
            _virusTimerCounter = 0;
            _virusPopCount++;

            if (_virusPopCount >= 3)
            {
                _virusPopCount = 0;
                _virusCooldownEndTime = DateTime.Now.AddMinutes(2);
                StatusMessage = "Viruses cleared! You have a 2-minute break.";
            }
            else
            {
                UpdateStatusMessage();
            }
        }

        [RelayCommand]
        private void OpenShop()
        {
            if (ActivePet.CanWearClothes)
            {
                _navigateTo(new ShopViewModel(ActivePet, _navigateTo));
            }
            else
            {
                StatusMessage = "No clothes available for this character yet!";
            }
        }

        [RelayCommand]
        private void OpenGames()
        {
            _gameTimer?.Stop();
            _navigateTo(new GamesMenuViewModel(ActivePet, _navigateTo));
        }

        [RelayCommand]
        private void BackToMenu()
        {
            _gameTimer?.Stop();
            
            string safeName = string.Join("_", ActivePet.Name.Split(Path.GetInvalidFileNameChars()));
            PetSaveManager.SavePet(ActivePet, $"{safeName}.json");
            
            _navigateTo(new MainMenuViewModel(_navigateTo));
        }

        private void UpdateGame()
        {
            bool wasSick = ActivePet.IsSick;
            ActivePet.UpdateState();

            bool isTutorialFinished = !ActivePet.IsTutorialActive || ActivePet.TutorialStep > 0;

            if (ActivePet is CsongorPet && ActivePet.IsAlive && !ActivePet.IsSick && isTutorialFinished)
            {
                if (IsVirusVisible)
                {
                    _virusTimerCounter++;
                    if (_virusTimerCounter >= 5)
                    {
                        IsVirusVisible = false;
                        _virusTimerCounter = 0;
                        _virusPopCount = 0; 
                        ActivePet.IsSick = true;
                        UpdateStatusMessage();
                    }
                }
                else if (DateTime.Now >= _virusCooldownEndTime)
                {
                    if (_random.Next(0, 20) == 0) 
                    {
                        VirusX = _random.Next(100, 1000);
                        VirusY = _random.Next(100, 500);
                        IsVirusVisible = true;
                        _virusTimerCounter = 0;
                        StatusMessage = "VIRUS DETECTED! POP IT QUICK!";
                    }
                }
            }
            else if (IsVirusVisible)
            {
                IsVirusVisible = false;
                _virusTimerCounter = 0;
            }
            
            if (wasSick != ActivePet.IsSick || !ActivePet.IsAlive)
            {
                UpdateStatusMessage();
            }

            OnPropertyChanged(nameof(ActivePet));
            OnPropertyChanged(nameof(ActivePetImage));

            if (!ActivePet.IsAlive)
            {
                _gameTimer?.Stop();
                string safeName = string.Join("_", ActivePet.Name.Split(Path.GetInvalidFileNameChars()));
                PetSaveManager.SavePet(ActivePet, $"{safeName}.json");
            }
        }

        private Bitmap LoadBitmap(string fileNameWithoutExtension)
        {
            string[] assemblyNames = { "Tamagotchi-NonchalanTeam", "Tamagotchi_NonchalanTeam" };
            string[] folders = { "Assets", "Images" };
            string[] extensions = { ".png", ".jpg", ".jpeg" };

            foreach (var assemblyName in assemblyNames)
            {
                foreach (var folder in folders)
                {
                    foreach (var ext in extensions)
                    {
                        var uri = new Uri($"avares://{assemblyName}/{folder}/{fileNameWithoutExtension}{ext}");
                        if (AssetLoader.Exists(uri))
                        {
                            return new Bitmap(AssetLoader.Open(uri));
                        }
                    }
                }
            }

            foreach (var assemblyName in assemblyNames)
            {
                var fallbackUri = new Uri($"avares://{assemblyName}/Assets/avalonia-logo.ico");
                if (AssetLoader.Exists(fallbackUri))
                {
                    return new Bitmap(AssetLoader.Open(fallbackUri));
                }
            }
            
            return null!; 
        }
    }
}