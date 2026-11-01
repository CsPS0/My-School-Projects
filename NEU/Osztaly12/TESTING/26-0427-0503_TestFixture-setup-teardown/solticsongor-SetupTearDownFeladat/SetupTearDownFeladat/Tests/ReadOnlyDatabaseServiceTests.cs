using NUnit.Framework;

namespace SetupTearDownFeladat.Tests
{
    [TestFixture]
    public class ReadOnlyDatabaseServiceTests : DatabaseServiceTests
    {
        [Test]
        public void ReadOnly_ShouldBeEmptyInitially()
        {
            Assert.That(_service.GetBookCount(), Is.EqualTo(0));
        }

        [Test]
        public void ReadOnly_Connection_Is_Active()
        {
            Assert.That(_service.IsConnected, Is.True);
        }
    }
}