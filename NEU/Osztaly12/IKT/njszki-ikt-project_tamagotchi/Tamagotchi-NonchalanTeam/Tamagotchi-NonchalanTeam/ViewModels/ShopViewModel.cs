using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public class ShopItem : ObservableObject
    {
        private bool _isSelected;
        private bool _isOwned;
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public int Price { get; set; }
        public string IconPath { get; set; } = "";

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public bool IsOwned
        {
            get => _isOwned;
            set => SetProperty(ref _isOwned, value);
        }
    }

    public partial class ShopViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _navigateTo;
        private readonly Pet _activePet;

        [ObservableProperty]
        private List<ShopItem> _shopItems;

        [ObservableProperty]
        private ShopItem? _selectedItem;

        [ObservableProperty]
        private string _characterImage = "";

        [ObservableProperty]
        private string _tutorialMessage = "";

        public Pet ActivePet => _activePet;

        public int SelectedPrice => SelectedItem?.Price ?? 0;

        public ShopViewModel(Pet pet, Action<ViewModelBase> navigateTo)
        {
            _navigateTo = navigateTo;
            _activePet = pet;
            ShopItems = new List<ShopItem>();
            LoadShopItems();
            UpdateCharacterImage();

            if (_activePet.IsTutorialActive && _activePet.TutorialStep == 0)
            {
                TutorialMessage = "Hey! Let's get you some clothes. You have 300 coins. Buy a hat, a shirt, and some pants to continue!";
            }
        }

        private void LoadShopItems()
        {
            var itemsData = PetSaveManager.LoadShopItems();
            ShopItems = itemsData.Select(item => new ShopItem
            {
                Id = item.Id,
                Name = item.Name,
                Type = item.Type.ToLower(),
                Price = item.Price,
                IconPath = LoadBitmapPath(item.IconPath),
                IsOwned = _activePet.OwnedClothes.Contains(item.Id)
            }).ToList();
        }
        
        private string LoadBitmapPath(string pathOrFileName)
        {
            string[] assemblyNames = { "Tamagotchi-NonchalanTeam", "Tamagotchi_NonchalanTeam" };

            if (pathOrFileName.StartsWith("avares://"))
            {
                foreach (var assemblyName in assemblyNames)
                {
                    var uriStr = pathOrFileName.Replace("Tamagotchi-NonchalanTeam", assemblyName);
                    var uri = new Uri(uriStr);
                    if (AssetLoader.Exists(uri)) return uri.ToString();
                }
                return "avares://Tamagotchi-NonchalanTeam/Assets/avalonia-logo.ico";
            }

            foreach (var assemblyName in assemblyNames)
            {
                var constructedUri = new Uri($"avares://{assemblyName}/Images/csps-statuses/shop-clothes/{pathOrFileName}");
                if (AssetLoader.Exists(constructedUri)) return constructedUri.ToString();
            }

            return "avares://Tamagotchi-NonchalanTeam/Assets/avalonia-logo.ico";
        }

        [RelayCommand]
        private void SelectItem(ShopItem item)
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsSelected = false;
            }
            SelectedItem = item;
            SelectedItem.IsSelected = true;
            OnPropertyChanged(nameof(SelectedPrice));
            UpdateCharacterImage();
        }

        private void UpdateCharacterImage()
        {
            string[] assemblyNames = { "Tamagotchi-NonchalanTeam", "Tamagotchi_NonchalanTeam" };
            
            if (_activePet.PetType == "Csongor")
            {
                string hat = _activePet.CurrentHat ?? "";
                string shirt = _activePet.CurrentShirt ?? "";
                string pants = _activePet.CurrentPants ?? "";

                if (SelectedItem != null && !SelectedItem.IsOwned)
                {
                    if (SelectedItem.Type == "hat") hat = SelectedItem.Id;
                    else if (SelectedItem.Type == "shirt") shirt = SelectedItem.Id;
                    else if (SelectedItem.Type == "pants") pants = SelectedItem.Id;
                }

                if (hat == "buvarmaszk" && pants == "rovidnadrag")
                {
                    hat = "buvarnaszk";
                }
                
                if (_activePet.IsTutorialActive)
                {
                    string tutorialImage = "1fazis-fazik";
                    bool hasHat = _activePet.OwnedClothes.Any(id => id.Contains("kalap") || id.Contains("maszk"));
                    bool hasShirt = _activePet.OwnedClothes.Any(id => id.Contains("polo"));
                    bool hasPants = _activePet.OwnedClothes.Any(id => id.Contains("nadrag") || id.Contains("farmer"));

                    if (SelectedItem != null)
                    {
                        if (SelectedItem.Type == "hat") hasHat = true;
                        else if (SelectedItem.Type == "shirt") hasShirt = true;
                        else if (SelectedItem.Type == "pants") hasPants = true;
                    }

                    if (hasHat && hasShirt && hasPants) tutorialImage = "3fazis-vegreszep";
                    else if (hasShirt || hasPants) tutorialImage = "2fazis-nokalap";
                    
                    foreach (var assemblyName in assemblyNames)
                    {
                        var uri = new Uri($"avares://{assemblyName}/Images/csps-statuses/meztelen-tutorial/{tutorialImage}.png");
                        if (AssetLoader.Exists(uri))
                        {
                            CharacterImage = uri.ToString();
                            return;
                        }
                    }
                }
                else
                {
                    foreach (var assemblyName in assemblyNames)
                    {
                        var uri = new Uri($"avares://{assemblyName}/Images/csps-statuses/{hat}-{shirt}-{pants}/semleges.png");
                        if (AssetLoader.Exists(uri))
                        {
                            CharacterImage = uri.ToString();
                            return;
                        }
                    }
                }
                CharacterImage = "avares://Tamagotchi-NonchalanTeam/Assets/avalonia-logo.ico";
            }
            else
            {
                foreach (var assemblyName in assemblyNames)
                {
                    var uri = new Uri($"avares://{assemblyName}/Images/lobster-statuses/lobster-semleges.png");
                    if (AssetLoader.Exists(uri))
                    {
                        CharacterImage = uri.ToString();
                        return;
                    }
                }
                CharacterImage = "avares://Tamagotchi-NonchalanTeam/Assets/avalonia-logo.ico";
            }
        }
        
        [RelayCommand]
        private void NextHat() => CycleOutfit("hat", 1);
        [RelayCommand]
        private void PreviousHat() => CycleOutfit("hat", -1);
        [RelayCommand]
        private void NextShirt() => CycleOutfit("shirt", 1);
        [RelayCommand]
        private void PreviousShirt() => CycleOutfit("shirt", -1);
        [RelayCommand]
        private void NextPants() => CycleOutfit("pants", 1);
        [RelayCommand]
        private void PreviousPants() => CycleOutfit("pants", -1);

        private void CycleOutfit(string type, int direction)
        {
            var available = _activePet.OwnedClothes
                .Where(id => ShopItems.Any(item => item.Id == id && item.Type == type))
                .ToList();
            
            available.Insert(0, "");

            string? current;
            switch (type)
            {
                case "hat": current = _activePet.CurrentHat; break;
                case "shirt": current = _activePet.CurrentShirt; break;
                case "pants": current = _activePet.CurrentPants; break;
                default: return;
            }

            int currentIndex = available.IndexOf(current ?? "");
            int nextIndex = (currentIndex + direction + available.Count) % available.Count;
            
            _activePet.Equip(type, available[nextIndex]);
            UpdateCharacterImage();
        }

        [RelayCommand]
        private void Buy()
        {
            if (SelectedItem == null || SelectedItem.IsOwned || _activePet.Money < SelectedItem.Price) return;

            _activePet.Money -= SelectedItem.Price;
            SelectedItem.IsOwned = true;
            _activePet.OwnedClothes.Add(SelectedItem.Id);

            if (_activePet.IsTutorialActive && _activePet.TutorialStep == 0)
            {
                bool hasHat = _activePet.OwnedClothes.Any(id => ShopItems.Any(item => item.Id == id && item.Type == "hat"));
                bool hasShirt = _activePet.OwnedClothes.Any(id => ShopItems.Any(item => item.Id == id && item.Type == "shirt"));
                bool hasPants = _activePet.OwnedClothes.Any(id => ShopItems.Any(item => item.Id == id && item.Type == "pants"));

                if (hasHat && hasShirt && hasPants)
                {
                    _activePet.TutorialStep = 1;
                    TutorialMessage = "Great! You look much better now. Let's go back to the game.";
                }
            }
        }

        [RelayCommand]
        private void Sell()
        {
            if (SelectedItem == null || !SelectedItem.IsOwned) return;

            _activePet.Money += SelectedItem.Price / 2;
            SelectedItem.IsOwned = false;
            _activePet.OwnedClothes.Remove(SelectedItem.Id);

            if (_activePet.CurrentHat == SelectedItem.Id) _activePet.Equip("hat", "");
            if (_activePet.CurrentShirt == SelectedItem.Id) _activePet.Equip("shirt", "");
            if (_activePet.CurrentPants == SelectedItem.Id) _activePet.Equip("pants", "");
            
            UpdateCharacterImage();
        }

        [RelayCommand]
        private void Back()
        {
            _navigateTo(new GameViewModel(_activePet, _navigateTo));
        }
    }
}