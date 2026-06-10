using System;

namespace Tamagotchi_NonchalanTeam.ViewModels
{
    public class ScoreEntry
    {
        public string Name { get; set; } = "";
        public TimeSpan SurvivalTime { get; set; }
        public string DisplayTime => $"{(int)SurvivalTime.TotalMinutes}:{SurvivalTime.Seconds:D2}";
    }
}
