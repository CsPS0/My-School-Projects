using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace tamagotchiLib
{
    public class PetSaveManager
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };
        
        public static string SaveDirectory
        {
            get
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TamagotchiNonchalanTeam", "Saves");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        
        public static List<ShopItemData> LoadShopItems()
        {
            var assembly = typeof(PetSaveManager).Assembly;
            var resourceName = "tamagotchiLib.shop_items.json";

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"Could not find the embedded resource: {resourceName}");

                using (var reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    return JsonSerializer.Deserialize<List<ShopItemData>>(json, JsonOptions) ?? new List<ShopItemData>();
                }
            }
        }

        public static void SavePet(Pet pet, string fileName)
        {
            if (pet == null)
                throw new ArgumentNullException(nameof(pet));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));

            string filePath = Path.Combine(SaveDirectory, fileName);
            try
            {
                var saveData = PetSaveData.FromPet(pet);
                string json = JsonSerializer.Serialize(saveData, JsonOptions);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to save pet to {filePath}", ex);
            }
        }

        public static Pet LoadPet(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));
            
            string filePath = Path.Combine(SaveDirectory, fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Save file not found: {filePath}");

            try
            {
                string json = File.ReadAllText(filePath);
                var saveData = JsonSerializer.Deserialize<PetSaveData>(json);

                if (saveData == null)
                    throw new InvalidOperationException("Failed to deserialize pet data");

                return saveData.ToPet();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Save file is corrupted or invalid JSON", ex);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to load pet from {filePath}", ex);
            }
        }

        public static async Task SavePetAsync(Pet pet, string fileName)
        {
            if (pet == null)
                throw new ArgumentNullException(nameof(pet));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));

            string filePath = Path.Combine(SaveDirectory, fileName);
            try
            {
                var saveData = PetSaveData.FromPet(pet);
                string json = JsonSerializer.Serialize(saveData, JsonOptions);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to save pet to {filePath}", ex);
            }
        }
        
        public static async Task<Pet> LoadPetAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));
            
            string filePath = Path.Combine(SaveDirectory, fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Save file not found: {filePath}");

            try
            {
                string json = await File.ReadAllTextAsync(filePath);
                var saveData = JsonSerializer.Deserialize<PetSaveData>(json);

                if (saveData == null)
                    throw new InvalidOperationException("Failed to deserialize pet data");

                return saveData.ToPet();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Save file is corrupted or invalid JSON", ex);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to load pet from {filePath}", ex);
            }
        }

        public static bool SaveFileExists(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            string filePath = Path.Combine(SaveDirectory, fileName);
            return File.Exists(filePath);
        }

        public static void DeleteSaveFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));

            string filePath = Path.Combine(SaveDirectory, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static string[] GetAllSaveFiles()
        {
            var files = Directory.GetFiles(SaveDirectory, "*.json");
            
            return files.Where(f => 
                !f.EndsWith(".deps.json", StringComparison.OrdinalIgnoreCase) && 
                !f.EndsWith(".runtimeconfig.json", StringComparison.OrdinalIgnoreCase)
            ).ToArray();
        }
        
        public static PetSaveData LoadSaveData(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));
            
            string filePath = Path.Combine(SaveDirectory, fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Save file not found: {filePath}");

            try
            {
                string json = File.ReadAllText(filePath);
                var saveData = JsonSerializer.Deserialize<PetSaveData>(json);

                if (saveData == null)
                    throw new InvalidOperationException("Failed to deserialize pet data");

                return saveData;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Save file is corrupted or invalid JSON", ex);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to load pet from {filePath}", ex);
            }
        }
    }
}
