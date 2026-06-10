using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace tamagotchiLib
{
    public abstract class Pet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _name = string.Empty;
        private int _hunger = 100;
        private int _happiness = 100;
        private int _health = 100;
        private int _age = 0;
        private bool _isAlive = true;
        private bool _isSick = false;
        private TimeSpan _survivalTime = TimeSpan.Zero;
        private bool _diedWithPride = false;

        public TimeSpan SurvivalTime
        {
            get => _survivalTime;
            set
            {
                if (_survivalTime != value)
                {
                    _survivalTime = value;
                    OnPropertyChanged(nameof(SurvivalTime));
                }
            }
        }

        public bool DiedWithPride
        {
            get => _diedWithPride;
            set
            {
                if (_diedWithPride != value)
                {
                    _diedWithPride = value;
                    OnPropertyChanged(nameof(DiedWithPride));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int Hunger
        {
            get => _hunger;
            internal set
            {
                if (_hunger != value)
                {
                    _hunger = value;
                    OnPropertyChanged(nameof(Hunger));
                }
            }
        }

        public int Happiness
        {
            get => _happiness;
            internal set
            {
                if (_happiness != value)
                {
                    _happiness = value;
                    OnPropertyChanged(nameof(Happiness));
                }
            }
        }







        public int Health
        {
            get => _health;
            internal set
            {
                if (_health != value)
                {
                    _health = value;
                    OnPropertyChanged(nameof(Health));
                }
            }
        }

        public int Age
        {
            get => _age;
            internal set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                if (_isAlive != value)
                {
                    _isAlive = value;
                    OnPropertyChanged(nameof(IsAlive));
                }
            }
        }

        public bool IsSick
        {
            get => _isSick;
            set
            {
                if (_isSick != value)
                {
                    _isSick = value;
                    OnPropertyChanged(nameof(IsSick));
                }
            }
        }

        private int _money = 300;
        public int Money
        {
            get => _money;
            set
            {
                if (_money != value)
                {
                    _money = value;
                    OnPropertyChanged(nameof(Money));
                }
            }
        }

        private int _foodStock = 5;
        public int FoodStock
        {
            get => _foodStock;
            set
            {
                if (_foodStock != value)
                {
                    _foodStock = value;
                    OnPropertyChanged(nameof(FoodStock));
                }
            }
        }

        private int _snakeHighScore = 0;
        public int SnakeHighScore
        {
            get => _snakeHighScore;
            set
            {
                if (_snakeHighScore != value)
                {
                    _snakeHighScore = value;
                    OnPropertyChanged(nameof(SnakeHighScore));
                }
            }
        }
        
        public string? CurrentHat { get; protected set; }
        public string? CurrentShirt { get; protected set; }
        public string? CurrentPants { get; protected set; }
        public ObservableCollection<string> OwnedClothes { get; } = new();

        public abstract bool CanWearClothes { get; }
        public abstract string PetType { get; }

        public bool IsTutorialActive { get; set; } = false;
        public int TutorialStep { get; set; } = 0;

        public int HungerDecreaseRate { get; set; } = 2;
        public int HappinessDecreaseRate { get; set; } = 1;
        protected bool CanGetSickRandomly { get; set; } = true;

        private int _ageUpdateCounter = 0;
        private const int AgeThreshold = 10; 
        private const int UpdatesPerAge = 60; 

        public event Action? PetDied;

        public void Feed()
        {
            if (!IsAlive) return;
            Hunger = Math.Min(100, Hunger + 20);
        }

        public void Heal()
        {
            if (!IsAlive) return;
            Health = Math.Min(100, Health + 30);
            IsSick = false;
        }

        public void IncreaseHappiness(int amount)
        {
            if (!IsAlive) return;
            Happiness = Math.Clamp(Happiness + amount, 0, 100);
        }

        
        
        
        
        
        public string Interact()
        {
            if (!IsAlive) return "Pet is no longer with us...";

            string actionName;
            int happinessBoost;

            if (Age < AgeThreshold)
            {
                
                actionName = "Play";
                happinessBoost = 15;
            }
            else
            {
                
                actionName = "Party";
                happinessBoost = 25; 
            }

            IncreaseHappiness(happinessBoost);
            return actionName;
        }

        private static readonly Random _random = new();

        public virtual void UpdateState()
        {
            if (!IsAlive) return;

            SurvivalTime = SurvivalTime.Add(TimeSpan.FromSeconds(1));
            if (SurvivalTime.TotalMinutes >= 5)
            {
                Die(true);
                return;
            }

            _ageUpdateCounter++;
            if (_ageUpdateCounter >= UpdatesPerAge)
            {
                Age++;
                _ageUpdateCounter = 0;
            }

            if (CanGetSickRandomly && !IsSick && _random.Next(0, 500) == 0)
            {
                IsSick = true;
            }

            int hungerDecay = IsSick ? HungerDecreaseRate * 2 : HungerDecreaseRate;
            int happinessDecay = IsSick ? HappinessDecreaseRate * 2 : HappinessDecreaseRate;

            Hunger = Math.Max(0, Hunger - hungerDecay);
            IncreaseHappiness(-(happinessDecay));

            if (Hunger == 0)
            {
                Health = Math.Max(0, Health - 5);
            }

            if (Health <= 0)
            {
                Die(false);
            }
        }

        public void ApplyTimeElapsed(TimeSpan elapsed)
        {
            if (!IsAlive) return;

            SurvivalTime = SurvivalTime.Add(elapsed);
            if (SurvivalTime.TotalMinutes >= 5)
            {
                SurvivalTime = TimeSpan.FromMinutes(5);
                Die(true);
                return;
            }

            int updates = (int)elapsed.TotalSeconds;
            if (updates <= 0) return;

            int hungerDecay = IsSick ? HungerDecreaseRate * 2 : HungerDecreaseRate;
            int happinessDecay = IsSick ? HappinessDecreaseRate * 2 : HappinessDecreaseRate;

            int oldHunger = Hunger;

            Hunger = Math.Max(0, Hunger - (updates * hungerDecay));
            Happiness = Math.Max(0, Happiness - (updates * happinessDecay));

            if (Hunger == 0)
            {
                int hungerSecondsToZero = hungerDecay > 0 ? (oldHunger / hungerDecay) : int.MaxValue;
                int healthDecaySeconds = Math.Max(0, updates - hungerSecondsToZero);
                
                Health = Math.Max(0, Health - (healthDecaySeconds * 5));
            }

            if (Health <= 0)
            {
                Die(false);
            }
        }

        protected void Die(bool withPride)
        {
            IsAlive = false;
            DiedWithPride = withPride;
            PetDied?.Invoke();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Equip(string itemType, string? itemId)
        {
            if (!CanWearClothes) return;

            switch (itemType.ToLower())
            {
                case "hat":
                    CurrentHat = itemId;
                    OnPropertyChanged(nameof(CurrentHat));
                    break;
                case "shirt":
                    CurrentShirt = itemId;
                    OnPropertyChanged(nameof(CurrentShirt));
                    break;
                case "pants":
                    CurrentPants = itemId;
                    OnPropertyChanged(nameof(CurrentPants));
                    break;
            }
        }
    }

    public class CsongorPet : Pet
    {
        public override bool CanWearClothes => true;
        public override string PetType => "Csongor";

        public CsongorPet()
        {
            HungerDecreaseRate = 1;
            HappinessDecreaseRate = 1;
            CanGetSickRandomly = false;
            Money = 300;
            IsTutorialActive = true;
            TutorialStep = 0;
        }
    }

    public class DavidPet : Pet
    {
        public override bool CanWearClothes => false;
        public override string PetType => "Dávid";

        public DavidPet()
        {
            HungerDecreaseRate = 4;
            HappinessDecreaseRate = 3;
            IsTutorialActive = false;
        }
    }
}

