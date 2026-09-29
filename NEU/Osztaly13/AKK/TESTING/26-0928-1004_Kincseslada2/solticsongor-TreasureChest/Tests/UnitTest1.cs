using TreasureChest;

namespace Tests
{
    public class Tests
    {
        private Chest chest = null!;

        [SetUp]
        public void Setup()
        {
            chest = new Chest("vasveretes faláda", 10);
        }

        private void OpenChest()
        {
            chest.UnLock();
            chest.Open();
        }

        [Test]
        public void isChestEmpty()
        {
            Assert.Pass();
        }

        [Test]
        public void isChestFull()
        {
            OpenChest();
            chest.Store(new Treasure("netherite", 10));
            Assert.That(chest.Store, Is.True, "Chest should be full.");
        }

        [Test]
        public void isChestLocked()
        {
            chest.Lock();
            Assert.That(chest.Lock(), Is.False, "Chest should not be able to lock again.");
            Assert.That(chest.IsLocked, Is.True, "Chest should be locked.");
        }

        [Test]
        public void isChestOpen()
        {
            chest.UnLock();
            Assert.That(chest.Open(), Is.True, "Chest should be able to open.");
            Assert.That(chest.IsOpen, Is.True, "Chest should be open.");
        }

        [Test]
        public void isTreasureWorthIt()
        {
            Assert.Pass();
        }

        [Test]
        public void isTreasureTooBig()
        {
            Assert.Pass();
        }
        
        public void isTreasureTooSmall()
        {
            Assert.Pass();
        }
    }
}
