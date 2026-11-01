using NUnit.Framework;

namespace SetupTearDownFeladat.Tests
{
    [TestFixture]
    public class DatabaseServiceTests
    {
        protected DatabaseService _service = null!;
        protected const string TestDbPath = "test_library.db";

        [SetUp]
        public void SetUp()
        {
            _service = new DatabaseService(TestDbPath);
            _service.OpenConnection();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.CloseConnection();
            if (File.Exists(TestDbPath))
            {
                File.Delete(TestDbPath);
            }
        }
        
        [Test]
        public void AddBook_ShouldIncreaseCount()
        {
            _service.AddBook("Egri Csillagok");
            int count = _service.GetBookCount();

            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Clear_ShouldRemoveAllBooks()
        {
            _service.AddBook("A Pál utcai fiúk");
            
            _service.Clear();
            
            Assert.That(_service.GetBookCount(), Is.EqualTo(0));
        }
        
        [Test]
        public void Test_Connection_Is_Active()
        {
            Assert.That(_service.IsConnected, Is.True);
        }
        
    }
}