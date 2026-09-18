using JatekLogika;
using NUnit.Framework;

namespace JatekLogika.Tests
{
    public class PlayerTests
    {
        [Test]
        public void Constructor_SetsIdNameAndSymbol()
        {
            var player = new Player(3, "Chris P. Bacon", Symbol.X);

            Assert.That(player.Id, Is.EqualTo(3));
            Assert.That(player.Name, Is.EqualTo("Chris P. Bacon"));
            Assert.That(player.Symbol, Is.EqualTo(Symbol.X));
        }

        [Test]
        public void FromCsvLine_ValidLine_ParsesIdAndName()
        {
            var player = Player.FromCsvLine("3;Chris P. Bacon", Symbol.O);

            Assert.That(player.Id, Is.EqualTo(3));
            Assert.That(player.Name, Is.EqualTo("Chris P. Bacon"));
            Assert.That(player.Symbol, Is.EqualTo(Symbol.O));
        }

        [TestCase("")]
        [TestCase("csak-egy-mezo")]
        [TestCase("nem-szam;Nev")]
        public void FromCsvLine_InvalidLine_ThrowsFormatException(string line)
        {
            Assert.Throws<System.FormatException>(() => Player.FromCsvLine(line, Symbol.X));
        }
    }
}
