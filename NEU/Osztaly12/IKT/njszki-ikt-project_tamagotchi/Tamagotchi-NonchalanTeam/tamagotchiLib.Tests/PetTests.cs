using Microsoft.VisualStudio.TestTools.UnitTesting;
using tamagotchiLib;

namespace tamagotchiLib.Tests
{
    [TestClass]
    public class PetTests
    {
        private Pet _pet = null!;

        [TestInitialize]
        public void Setup()
        {
            _pet = new CsongorPet();
        }

        [TestMethod]
        public void Constructor_InitializesWithDefaultValues()
        {
            Assert.AreEqual("", _pet.Name);
            Assert.AreEqual(100, _pet.Hunger);
            Assert.AreEqual(100, _pet.Happiness);
            Assert.AreEqual(100, _pet.Health);
            Assert.AreEqual(0, _pet.Age);
            Assert.IsTrue(_pet.IsAlive);
        }

        [TestMethod]
        public void Feed_IncreasesHunger()
        {
            _pet.UpdateState();
            _pet.UpdateState();
            int initialHunger = _pet.Hunger;
            
            _pet.Feed();
            
            Assert.IsTrue(_pet.Hunger > initialHunger);
            Assert.IsTrue(_pet.Hunger <= 100);
        }

        [TestMethod]
        public void Feed_CapsHungerAt100()
        {
            _pet.Feed();
            _pet.Feed();
            
            Assert.AreEqual(100, _pet.Hunger);
        }

        [TestMethod]
        public void Heal_IncreasesHealth()
        {
            for (int i = 0; i < 110; i++)
            {
                _pet.UpdateState();
            }
            int initialHealth = _pet.Health;
            
            _pet.Heal();
            
            Assert.IsTrue(_pet.Health > initialHealth);
            Assert.IsTrue(_pet.Health <= 100);
        }

        [TestMethod]
        public void Heal_CuresSickness()
        {
            _pet.IsSick = true;
            _pet.Heal();
            Assert.IsFalse(_pet.IsSick);
        }

        [TestMethod]
        public void UpdateState_DecreasesResources()
        {
            int initialHunger = _pet.Hunger;
            int initialHappiness = _pet.Happiness;
            
            _pet.UpdateState();
            
            Assert.IsTrue(_pet.Hunger < initialHunger);
            Assert.IsTrue(_pet.Happiness < initialHappiness);
        }

        [TestMethod]
        public void UpdateState_DecreasesHealthWhenResourcesLow()
        {
            for (int i = 0; i < 110; i++)
            {
                _pet.UpdateState();
            }
            
            int healthBefore = _pet.Health;
            _pet.UpdateState();
            
            Assert.IsTrue(_pet.Health < healthBefore);
        }

        [TestMethod]
        public void Die_SetsIsAliveToFalse()
        {
            for (int i = 0; i < 200; i++)
            {
                _pet.UpdateState();
            }
            
            Assert.IsFalse(_pet.IsAlive);
        }

        [TestMethod]
        public void PetDied_EventIsRaised()
        {
            bool eventRaised = false;
            _pet.PetDied += () => eventRaised = true;
            
            for (int i = 0; i < 200; i++)
            {
                _pet.UpdateState();
            }
            
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void Feed_DoesNothingIfDead()
        {
            for (int i = 0; i < 200; i++)
            {
                _pet.UpdateState();
            }
            
            int hunger = _pet.Hunger;
            _pet.Feed();
            
            Assert.AreEqual(hunger, _pet.Hunger);
        }

        [TestMethod]
        public void Heal_DoesNothingIfDead()
        {
            for (int i = 0; i < 200; i++)
            {
                _pet.UpdateState();
            }
            
            int health = _pet.Health;
            _pet.Heal();
            
            Assert.AreEqual(health, _pet.Health);
        }

        [TestMethod]
        public void UpdateState_DoesNothingIfDead()
        {
            for (int i = 0; i < 200; i++)
            {
                _pet.UpdateState();
            }
            
            int hunger = _pet.Hunger;
            _pet.UpdateState();
            
            Assert.AreEqual(hunger, _pet.Hunger);
        }

        [TestMethod]
        public void Hunger_TriggersPropertyChanged()
        {
            var changedProperties = new System.Collections.Generic.List<string>();
            _pet.PropertyChanged += (s, e) => changedProperties.Add(e.PropertyName!);
            
            _pet.UpdateState();
            
            Assert.IsTrue(changedProperties.Contains(nameof(Pet.Hunger)));
        }

        [TestMethod]
        public void Health_CanBeDecreasedCorrectly()
        {
            for (int i = 0; i < 110; i++)
            {
                _pet.UpdateState();
            }

            int health = _pet.Health;
            _pet.UpdateState();
            Assert.IsTrue(_pet.Health < health);
        }

        [TestMethod]
        public void CsongorPet_UpdateState_DoesNotRandomlySetSickness()
        {
            
            
            for (int i = 0; i < 1000; i++)
            {
                _pet.UpdateState();
                Assert.IsFalse(_pet.IsSick);
            }
        }

        [TestMethod]
        public void DavidPet_UpdateState_CanRandomlySetSickness()
        {
            Pet david = new DavidPet();
            bool gotSick = false;
            for (int i = 0; i < 10000; i++)
            {
                david.UpdateState();
                if (david.IsSick)
                {
                    gotSick = true;
                    break;
                }
                
                if (david.Hunger < 50) david.Feed();
                if (david.Health < 50) david.Heal();
                david.SurvivalTime = TimeSpan.Zero;
            }
            Assert.IsTrue(gotSick, "David should have eventually gotten sick via the base random logic.");
        }

        
        [TestMethod]
        public void Age_StartsAtZero()
        {
            Assert.AreEqual(0, _pet.Age);
        }

        [TestMethod]
        public void Age_IncrementsAfterUpdatesCycles()
        {
            
            for (int i = 0; i < 60; i++)
            {
                _pet.UpdateState();
                
                if (_pet.Hunger < 50) _pet.Feed();
                if (_pet.Health < 50) _pet.Heal();
            }
            
            Assert.AreEqual(1, _pet.Age);
        }

        [TestMethod]
        public void Age_ContinuesToIncrement()
        {
            
            for (int i = 0; i < 180; i++)
            {
                _pet.UpdateState();
                if (_pet.Hunger < 50) _pet.Feed();
                if (_pet.Health < 50) _pet.Heal();
            }
            
            Assert.AreEqual(3, _pet.Age);
        }

        
        [TestMethod]
        public void Interact_YoungPet_ReturnsPlayAction()
        {
            
            string action = _pet.Interact();
            Assert.AreEqual("Play", action);
        }

        [TestMethod]
        public void Interact_YoungPet_IncreasesHappinessByFifteen()
        {
            
            _pet.IncreaseHappiness(-85); 
            int initialHappiness = _pet.Happiness;
            _pet.Interact();
            
            Assert.AreEqual(initialHappiness + 15, _pet.Happiness);
        }

        [TestMethod]
        public void Interact_GrownPet_ReturnsPartyAction()
        {
            
            
            for (int i = 0; i < 1000; i++)
            {
                _pet.UpdateState();
                
                if (_pet.Hunger < 80) _pet.Feed();
                if (_pet.Health < 80) _pet.Heal();
                if (_pet.IsSick) _pet.Heal();
            }
            
            
            if (_pet.Age >= 10)
            {
                string action = _pet.Interact();
                Assert.AreEqual("Party", action);
            }
        }

        [TestMethod]
        public void Interact_GrownPet_IncreasesHappinessByTwentyFive()
        {
            
            for (int i = 0; i < 1000; i++)
            {
                _pet.UpdateState();
                if (_pet.Hunger < 80) _pet.Feed();
                if (_pet.Health < 80) _pet.Heal();
                if (_pet.IsSick) _pet.Heal();
            }
            
            
            if (_pet.Age >= 10)
            {
                _pet.IncreaseHappiness(-75); 
                int initialHappiness = _pet.Happiness;
                _pet.Interact();
                
                Assert.AreEqual(initialHappiness + 25, _pet.Happiness);
            }
        }

        [TestMethod]
        public void Interact_DeadPet_ReturnsDeadMessage()
        {
            
            for (int i = 0; i < 200; i++)
            {
                _pet.UpdateState();
            }
            
            Assert.IsFalse(_pet.IsAlive);
            string result = _pet.Interact();
            Assert.AreEqual("Pet is no longer with us...", result);
        }

        [TestMethod]
        public void Interact_DeadPet_DoesNotChangeHappiness()
        {
            
            for (int i = 0; i < 200; i++)
            {
                _pet.UpdateState();
            }
            
            int happiness = _pet.Happiness;
            _pet.Interact();
            Assert.AreEqual(happiness, _pet.Happiness);
        }

        [TestMethod]
        public void Interact_HappinessCaps_AtHundred()
        {
            
            _pet.IncreaseHappiness(200);
            Assert.AreEqual(100, _pet.Happiness);
            
            
            _pet.Interact();
            Assert.AreEqual(100, _pet.Happiness);
        }

        [TestMethod]
        public void Pet_DiesAtFiveMinutes()
        {
            Assert.IsTrue(_pet.IsAlive);
            
            
            _pet.ApplyTimeElapsed(TimeSpan.FromMinutes(5));
            
            
            Assert.IsFalse(_pet.IsAlive);
            Assert.IsTrue(_pet.DiedWithPride);
            Assert.AreEqual(5, _pet.SurvivalTime.TotalMinutes);
        }
    }
}
