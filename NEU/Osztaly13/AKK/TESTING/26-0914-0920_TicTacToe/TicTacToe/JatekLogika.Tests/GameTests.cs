using JatekLogika;
using NUnit.Framework;

namespace JatekLogika.Tests
{
    public class GameTests
    {
        private Player _playerX;
        private Player _playerO;
        private Game _game;

        [SetUp]
        public void Setup()
        {
            _playerX = new Player(1, "Anita Job", Symbol.X);
            _playerO = new Player(2, "Barry Cade", Symbol.O);
            _game = new Game(_playerX, _playerO);
        }

        [Test]
        public void Constructor_RequiresOneXAndOneOPlayer()
        {
            var bothX1 = new Player(1, "A", Symbol.X);
            var bothX2 = new Player(2, "B", Symbol.X);

            Assert.Throws<System.ArgumentException>(() => new Game(bothX1, bothX2));
        }

        [Test]
        public void NewGame_StatusIsInProgress()
        {
            Assert.That(_game.Status, Is.EqualTo(GameStatus.InProgress));
        }

        [Test]
        public void NewGame_XPlayerStartsFirst()
        {
            Assert.That(_game.CurrentPlayer, Is.EqualTo(_playerX));
        }

        [Test]
        public void MakeMove_ValidMove_SwitchesToOtherPlayer()
        {
            bool result = _game.MakeMove(0, 0);

            Assert.That(result, Is.True);
            Assert.That(_game.CurrentPlayer, Is.EqualTo(_playerO));
            Assert.That(_game.Board.GetCell(0, 0), Is.EqualTo(Symbol.X));
        }

        [Test]
        public void MakeMove_OnOccupiedCell_ReturnsFalseAndKeepsSamePlayer()
        {
            _game.MakeMove(0, 0);

            bool result = _game.MakeMove(0, 0);

            Assert.That(result, Is.False);
            Assert.That(_game.CurrentPlayer, Is.EqualTo(_playerO));
        }

        [Test]
        public void MakeMove_AfterGameEnded_ReturnsFalse()
        {
            // X: (0,0) (0,1) (0,2) -> horizontal win
            _game.MakeMove(0, 0); // X
            _game.MakeMove(1, 0); // O
            _game.MakeMove(0, 1); // X
            _game.MakeMove(1, 1); // O
            _game.MakeMove(0, 2); // X wins

            bool result = _game.MakeMove(2, 2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void MakeMove_WinningMove_SetsStatusToXWins()
        {
            _game.MakeMove(0, 0); // X
            _game.MakeMove(1, 0); // O
            _game.MakeMove(0, 1); // X
            _game.MakeMove(1, 1); // O
            _game.MakeMove(0, 2); // X wins horizontally

            Assert.That(_game.Status, Is.EqualTo(GameStatus.XWins));
        }

        [Test]
        public void MakeMove_WinningMoveForO_SetsStatusToOWins()
        {
            _game.MakeMove(0, 0); // X
            _game.MakeMove(1, 0); // O
            _game.MakeMove(0, 1); // X
            _game.MakeMove(1, 1); // O
            _game.MakeMove(2, 2); // X (no win)
            _game.MakeMove(1, 2); // O wins vertically

            Assert.That(_game.Status, Is.EqualTo(GameStatus.OWins));
        }

        [Test]
        public void MakeMove_BoardFullNoWinner_SetsStatusToDraw()
        {
            // X O X
            // X O O
            // O X X
            _game.MakeMove(0, 0); // X
            _game.MakeMove(0, 1); // O
            _game.MakeMove(0, 2); // X
            _game.MakeMove(1, 1); // O
            _game.MakeMove(1, 0); // X
            _game.MakeMove(1, 2); // O
            _game.MakeMove(2, 1); // X
            _game.MakeMove(2, 0); // O
            _game.MakeMove(2, 2); // X

            Assert.That(_game.Status, Is.EqualTo(GameStatus.Draw));
        }

        [Test]
        public void GetWinnerPlayer_AfterXWins_ReturnsPlayerX()
        {
            _game.MakeMove(0, 0); // X
            _game.MakeMove(1, 0); // O
            _game.MakeMove(0, 1); // X
            _game.MakeMove(1, 1); // O
            _game.MakeMove(0, 2); // X wins

            Assert.That(_game.GetWinnerPlayer(), Is.EqualTo(_playerX));
        }

        [Test]
        public void GetWinnerPlayer_GameInProgress_ReturnsNull()
        {
            Assert.That(_game.GetWinnerPlayer(), Is.Null);
        }

        [Test]
        public void Reset_ReturnsToStartingState()
        {
            _game.MakeMove(0, 0);

            _game.Reset();

            Assert.That(_game.Status, Is.EqualTo(GameStatus.InProgress));
            Assert.That(_game.CurrentPlayer, Is.EqualTo(_playerX));
            Assert.That(_game.Board.GetCell(0, 0), Is.EqualTo(Symbol.None));
        }
    }
}
