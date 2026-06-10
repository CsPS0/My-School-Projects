using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using tamagotchiLib;

namespace tamagotchiLib.Tests
{
    [TestClass]
    public class PetSaveLoadTests
    {
        private string _testDirectory = null!;
        private string _testFilePath = null!;

        [TestInitialize]
        public void Setup()
        {
            _testDirectory = Path.Combine(Path.GetTempPath(), "PetSaveLoadTests");
            Directory.CreateDirectory(_testDirectory);
            _testFilePath = Path.Combine(_testDirectory, "test_pet.json");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        [TestMethod]
        public void SavePet_CreatesJsonFile()
        {
            var pet = new CsongorPet { Name = "TestPet" };

            PetSaveManager.SavePet(pet, _testFilePath);

            Assert.IsTrue(File.Exists(_testFilePath));
        }

        [TestMethod]
        public void SavePet_ContainsCorrectData()
        {
            var pet = new CsongorPet { Name = "Csongor" };
            pet.UpdateState();

            PetSaveManager.SavePet(pet, _testFilePath);
            string json = File.ReadAllText(_testFilePath);

            Assert.IsTrue(json.Contains("Csongor"));
            Assert.IsTrue(json.Contains("Hunger"));
            Assert.IsFalse(json.Contains("Thirst"));
        }

        [TestMethod]
        public void LoadPet_ReturnsCorrectPet()
        {
            var originalPet = new CsongorPet { Name = "TestPet" };
            originalPet.UpdateState();
            originalPet.Feed();
            PetSaveManager.SavePet(originalPet, _testFilePath);

            var loadedPet = PetSaveManager.LoadPet(_testFilePath);

            Assert.AreEqual(originalPet.Name, loadedPet.Name);
            Assert.AreEqual(originalPet.Hunger, loadedPet.Hunger);
            Assert.AreEqual(originalPet.Happiness, loadedPet.Happiness);
            Assert.AreEqual(originalPet.Health, loadedPet.Health);
            Assert.AreEqual(originalPet.IsAlive, loadedPet.IsAlive);
        }

        [TestMethod]
        public void SaveAndLoadPet_PreservesState()
        {
            var originalPet = new DavidPet { Name = "Dávid" };
            for (int i = 0; i < 5; i++)
            {
                originalPet.UpdateState();
            }
            originalPet.IsSick = true;

            PetSaveManager.SavePet(originalPet, _testFilePath);
            var loadedPet = PetSaveManager.LoadPet(_testFilePath);

            Assert.AreEqual(originalPet.Name, loadedPet.Name);
            Assert.AreEqual(originalPet.Hunger, loadedPet.Hunger);
            Assert.AreEqual(originalPet.Happiness, loadedPet.Happiness);
            Assert.AreEqual(originalPet.Health, loadedPet.Health);
            Assert.AreEqual(originalPet.Age, loadedPet.Age);
            Assert.AreEqual(originalPet.IsAlive, loadedPet.IsAlive);
            Assert.AreEqual(originalPet.IsSick, loadedPet.IsSick);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadPet_ThrowsExceptionWhenFileNotFound()
        {
            PetSaveManager.LoadPet(Path.Combine(_testDirectory, "nonexistent.json"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SavePet_ThrowsExceptionForEmptyPath()
        {
            var pet = new CsongorPet { Name = "Test" };

            PetSaveManager.SavePet(pet, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SavePet_ThrowsExceptionForNullPet()
        {
            PetSaveManager.SavePet(null!, _testFilePath);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LoadPet_ThrowsExceptionForCorruptedJson()
        {
            File.WriteAllText(_testFilePath, "{ invalid json }");

            PetSaveManager.LoadPet(_testFilePath);
        }

        [TestMethod]
        public void SaveFileExists_ReturnsTrueForExistingFile()
        {
            var pet = new CsongorPet { Name = "Test" };
            PetSaveManager.SavePet(pet, _testFilePath);

            bool exists = PetSaveManager.SaveFileExists(_testFilePath);

            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void SaveFileExists_ReturnsFalseForNonexistentFile()
        {
            bool exists = PetSaveManager.SaveFileExists(Path.Combine(_testDirectory, "nonexistent.json"));

            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void DeleteSaveFile_RemovesFile()
        {
            var pet = new CsongorPet { Name = "Test" };
            PetSaveManager.SavePet(pet, _testFilePath);
            Assert.IsTrue(File.Exists(_testFilePath));

            PetSaveManager.DeleteSaveFile(_testFilePath);

            Assert.IsFalse(File.Exists(_testFilePath));
        }

        [TestMethod]
        public void SavePet_WithDeadPet_PreservesDeadState()
        {
            var pet = new CsongorPet { Name = "DeadPet" };
            for (int i = 0; i < 200; i++)
            {
                pet.UpdateState();
            }
            Assert.IsFalse(pet.IsAlive);

            PetSaveManager.SavePet(pet, _testFilePath);
            var loadedPet = PetSaveManager.LoadPet(_testFilePath);

            Assert.IsFalse(loadedPet.IsAlive);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task SavePetAsync_SavesCorrectly()
        {
            var pet = new CsongorPet { Name = "AsyncTest" };

            await PetSaveManager.SavePetAsync(pet, _testFilePath);

            Assert.IsTrue(File.Exists(_testFilePath));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task LoadPetAsync_LoadsCorrectly()
        {
            var originalPet = new CsongorPet { Name = "AsyncLoadTest" };
            await PetSaveManager.SavePetAsync(originalPet, _testFilePath);

            var loadedPet = await PetSaveManager.LoadPetAsync(_testFilePath);

            Assert.AreEqual(originalPet.Name, loadedPet.Name);
        }

        [TestMethod]
        public void PetSaveData_FromPet_CreatesValidData()
        {
            var pet = new CsongorPet { Name = "DataTest" };
            pet.UpdateState();

            var saveData = PetSaveData.FromPet(pet);

            Assert.AreEqual(pet.Name, saveData.Name);
            Assert.AreEqual(pet.Hunger, saveData.Hunger);
            Assert.AreEqual(pet.Happiness, saveData.Happiness);
            Assert.AreEqual(pet.Health, saveData.Health);
            Assert.AreEqual(pet.IsAlive, saveData.IsAlive);
        }

        [TestMethod]
        public void PetSaveData_ToPet_RestoresAllProperties()
        {
            var saveData = new PetSaveData
            {
                Name = "RestoredPet",
                PetType = "Csongor",
                Hunger = 80,
                Happiness = 90,
                Health = 95,
                Age = 5,
                IsAlive = true,
                IsSick = true,
                LastSaved = DateTime.Now
            };

            var pet = saveData.ToPet();

            Assert.AreEqual("RestoredPet", pet.Name);
            Assert.AreEqual(80, pet.Hunger);
            Assert.AreEqual(90, pet.Happiness);
            Assert.AreEqual(95, pet.Health);
            Assert.AreEqual(5, pet.Age);
            Assert.IsTrue(pet.IsAlive);
            Assert.IsTrue(pet.IsSick);
        }
        [TestMethod]
        public void LoadShopItems_ReturnsItems()
        {
            var items = PetSaveManager.LoadShopItems();

            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
            Assert.IsFalse(string.IsNullOrEmpty(items[0].Id));
            Assert.IsFalse(string.IsNullOrEmpty(items[0].Name));
        }
    }
}
