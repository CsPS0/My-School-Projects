using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using tamagotchiLib;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public partial class ScoreboardViewModel : ViewModelBase
    {
        private readonly Action<ViewModelBase> _navigateTo;

        [ObservableProperty]
        private ObservableCollection<ScoreEntry> _scoreboard = new();

        public ScoreboardViewModel(Action<ViewModelBase> navigateTo)
        {
            _navigateTo = navigateTo;
            LoadScoreboard();
        }

        private void LoadScoreboard()
        {
            try
            {
                var directory = PetSaveManager.SaveDirectory;
                if (!Directory.Exists(directory)) return;

                var saveFiles = Directory.GetFiles(directory, "*.json")
                    .Where(f => !f.EndsWith(".deps.json") && !f.EndsWith(".runtimeconfig.json"));

                var scores = new List<ScoreEntry>();

                foreach (var file in saveFiles)
                {
                    try
                    {
                        var fileName = Path.GetFileName(file);
                        var pet = PetSaveManager.LoadPet(fileName);
                        
                        if (!pet.IsAlive)
                        {
                            scores.Add(new ScoreEntry 
                            { 
                                Name = pet.Name, 
                                SurvivalTime = pet.SurvivalTime 
                            });
                        }
                    }
                    catch { }
                }

                Scoreboard = new ObservableCollection<ScoreEntry>(
                    scores.OrderByDescending(s => s.SurvivalTime).Take(10));
            }
            catch { }
        }

        [RelayCommand]
        private void Back()
        {
            _navigateTo(new MainMenuViewModel(_navigateTo));
        }
    }
}

