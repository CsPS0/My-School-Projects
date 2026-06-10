using System;
using System.Collections.Generic;
using System.Linq;

namespace tamagotchiLib
{
    public class ShopItemData
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public int Price { get; set; }
        public string IconPath { get; set; } = "";
    }
    
    public class PetSaveData
    {
        public string Name { get; set; } = string.Empty;
        public string PetType { get; set; } = string.Empty;
        public int Hunger { get; set; }
        public int Happiness { get; set; }
        public int Health { get; set; }
        public int Age { get; set; }
        public bool IsAlive { get; set; }
        public int Money { get; set; }
        public int FoodStock { get; set; }
        public int SnakeHighScore { get; set; }
        
        public string? CurrentHat { get; set; }
        public string? CurrentShirt { get; set; }
        public string? CurrentPants { get; set; }
        public List<string> OwnedClothes { get; set; } = new();

        public bool IsSick { get; set; }
        public TimeSpan SurvivalTime { get; set; }
        public bool DiedWithPride { get; set; }
        public DateTime LastSaved { get; set; } = DateTime.Now;

        public static PetSaveData FromPet(Pet pet)
        {
            return new PetSaveData
            {
                Name = pet.Name,
                PetType = pet.PetType,
                Hunger = pet.Hunger,
                Happiness = pet.Happiness,
                Health = pet.Health,
                Age = pet.Age,
                IsAlive = pet.IsAlive,
                Money = pet.Money,
                FoodStock = pet.FoodStock,
                SnakeHighScore = pet.SnakeHighScore,
                CurrentHat = pet.CurrentHat,
                CurrentShirt = pet.CurrentShirt,
                CurrentPants = pet.CurrentPants,
                OwnedClothes = pet.OwnedClothes.ToList(),
                IsSick = pet.IsSick,
                SurvivalTime = pet.SurvivalTime,
                DiedWithPride = pet.DiedWithPride,
                LastSaved = DateTime.Now,
            };
        }

        public Pet ToPet()
        {
            Pet pet = PetType switch
            {
                "Csongor" => new CsongorPet(),
                "Dávid" => new DavidPet(),
                _ => throw new InvalidOperationException($"Unknown pet type: {PetType}")
            };

            pet.Name = Name;
            pet.Hunger = Hunger;
            pet.Happiness = Happiness;
            pet.Health = Health;
            pet.Age = Age;
            pet.IsAlive = IsAlive;
            pet.IsSick = IsSick;
            pet.SurvivalTime = SurvivalTime;
            pet.DiedWithPride = DiedWithPride;
            pet.Money = Money;
            pet.FoodStock = FoodStock;
            pet.SnakeHighScore = SnakeHighScore;
            
            OwnedClothes.ForEach(pet.OwnedClothes.Add);
            pet.Equip("hat", CurrentHat);
            pet.Equip("shirt", CurrentShirt);
            pet.Equip("pants", CurrentPants);

            if (IsAlive)
            {
                TimeSpan elapsed = DateTime.Now - LastSaved;
                pet.ApplyTimeElapsed(elapsed);
            }

            return pet;
        }
    }
}