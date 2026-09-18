using JatekLogika;
using NUnit.Framework;

namespace JatekLogika.Tests
{
    public class GameBoardTests
    {
        private GameBoard _board;

        [SetUp]
        public void Setup()
        {
            _board = new GameBoard();
        }

        [Test]
        public void NewBoard_AllCellsAreEmpty()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Assert.That(_board.GetCell(row, col), Is.EqualTo(Symbol.None));
                }
            }
        }

        [Test]
        public void PlaceMark_OnEmptyCell_ReturnsTrueAndSetsSymbol()
        {
            bool result = _board.PlaceMark(0, 1, Symbol.X);

            Assert.That(result, Is.True);
            Assert.That(_board.GetCell(0, 1), Is.EqualTo(Symbol.X));
        }

        [Test]
        public void PlaceMark_OnOccupiedCell_ReturnsFalseAndKeepsOriginalSymbol()
        {
            _board.PlaceMark(0, 1, Symbol.X);

            bool result = _board.PlaceMark(0, 1, Symbol.O);

            Assert.That(result, Is.False);
            Assert.That(_board.GetCell(0, 1), Is.EqualTo(Symbol.X));
        }

        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(3, 0)]
        [TestCase(0, 3)]
        public void PlaceMark_OutOfRangeCoordinates_ReturnsFalse(int row, int col)
        {
            bool result = _board.PlaceMark(row, col, Symbol.X);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IsCellEmpty_EmptyCell_ReturnsTrue()
        {
            Assert.That(_board.IsCellEmpty(1, 1), Is.True);
        }

        [Test]
        public void IsCellEmpty_OccupiedCell_ReturnsFalse()
        {
            _board.PlaceMark(1, 1, Symbol.O);

            Assert.That(_board.IsCellEmpty(1, 1), Is.False);
        }

        [Test]
        public void IsFull_EmptyBoard_ReturnsFalse()
        {
            Assert.That(_board.IsFull(), Is.False);
        }

        [Test]
        public void IsFull_AllCellsFilled_ReturnsTrue()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    _board.PlaceMark(row, col, Symbol.X);
                }
            }

            Assert.That(_board.IsFull(), Is.True);
        }

        [Test]
        public void Reset_ClearsAllCells()
        {
            _board.PlaceMark(0, 0, Symbol.X);

            _board.Reset();

            Assert.That(_board.GetCell(0, 0), Is.EqualTo(Symbol.None));
        }

        [TestCase(0, 0, 0, 1, 0, 2)]
        [TestCase(1, 0, 1, 1, 1, 2)]
        [TestCase(2, 0, 2, 1, 2, 2)]
        public void GetWinner_ThreeInARow_ReturnsThatSymbol(int r1, int c1, int r2, int c2, int r3, int c3)
        {
            _board.PlaceMark(r1, c1, Symbol.X);
            _board.PlaceMark(r2, c2, Symbol.X);
            _board.PlaceMark(r3, c3, Symbol.X);

            Assert.That(_board.GetWinner(), Is.EqualTo(Symbol.X));
        }

        [TestCase(0, 0, 1, 0, 2, 0)]
        [TestCase(0, 1, 1, 1, 2, 1)]
        [TestCase(0, 2, 1, 2, 2, 2)]
        public void GetWinner_ThreeInAColumn_ReturnsThatSymbol(int r1, int c1, int r2, int c2, int r3, int c3)
        {
            _board.PlaceMark(r1, c1, Symbol.O);
            _board.PlaceMark(r2, c2, Symbol.O);
            _board.PlaceMark(r3, c3, Symbol.O);

            Assert.That(_board.GetWinner(), Is.EqualTo(Symbol.O));
        }

        [Test]
        public void GetWinner_MainDiagonal_ReturnsThatSymbol()
        {
            _board.PlaceMark(0, 0, Symbol.X);
            _board.PlaceMark(1, 1, Symbol.X);
            _board.PlaceMark(2, 2, Symbol.X);

            Assert.That(_board.GetWinner(), Is.EqualTo(Symbol.X));
        }

        [Test]
        public void GetWinner_AntiDiagonal_ReturnsThatSymbol()
        {
            _board.PlaceMark(0, 2, Symbol.O);
            _board.PlaceMark(1, 1, Symbol.O);
            _board.PlaceMark(2, 0, Symbol.O);

            Assert.That(_board.GetWinner(), Is.EqualTo(Symbol.O));
        }

        [Test]
        public void GetWinner_NoWinningLine_ReturnsNone()
        {
            _board.PlaceMark(0, 0, Symbol.X);
            _board.PlaceMark(0, 1, Symbol.O);
            _board.PlaceMark(0, 2, Symbol.X);

            Assert.That(_board.GetWinner(), Is.EqualTo(Symbol.None));
        }
    }
}
