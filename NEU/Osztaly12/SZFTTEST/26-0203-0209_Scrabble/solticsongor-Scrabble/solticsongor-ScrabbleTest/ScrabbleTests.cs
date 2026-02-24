using solticsongor_ScrabbleCLI;

namespace solticsongor_ScrabbleTest
{
    public class FakeValidator : IWordValidator
    {
        public bool IsValidResult { get; set; } = true;
        public bool IsValid(string word)
        {
            return IsValidResult;
        }
    }

    public class ScrabbleTests
    {
        private Board _board;
        private Scorer _scorer;
        private FakeValidator _validator;

        public ScrabbleTests()
        {
            _board = new Board();
            _validator = new FakeValidator();
            _scorer = new Scorer(_validator);
        }

        [Fact]
        public void CalculateScore_SimpleWord_ReturnsSumOfValues()
        {
            var tiles = new List<(int, int, Tile)>
            {
                (7, 7, new Tile('C', 3)),
                (7, 8, new Tile('A', 1)),
                (7, 9, new Tile('T', 1))
            };

            int score = _scorer.CalculateScore(_board, tiles);

            Assert.Equal(5, score);
        }

        [Fact]
        public void CalculateScore_Extension_AddsToPreviousWord()
        {
            _board.PlaceTile(7, 7, new Tile('C', 3));
            _board.PlaceTile(7, 8, new Tile('A', 1));
            _board.PlaceTile(7, 9, new Tile('T', 1));

            var tiles = new List<(int, int, Tile)>
            {
                (7, 10, new Tile('S', 1))
            };

            int score = _scorer.CalculateScore(_board, tiles);

            Assert.Equal(6, score);
        }

        [Fact]
        public void CalculateScore_DoubleLetterBonus_DoublesSpecificTile()
        {
            _board.SetBonus(7, 7, BonusType.DoubleLetter);

            var tiles = new List<(int, int, Tile)>
            {
                (7, 7, new Tile('C', 3)),
                (7, 8, new Tile('A', 1))
            };

            int score = _scorer.CalculateScore(_board, tiles);

            Assert.Equal(7, score);
        }

        [Fact]
        public void CalculateScore_TripleWordBonus_TriplesTotalScore()
        {
            _board.SetBonus(7, 9, BonusType.TripleWord);

            var tiles = new List<(int, int, Tile)>
            {
                (7, 7, new Tile('C', 3)),
                (7, 8, new Tile('A', 1)),
                (7, 9, new Tile('T', 1))
            };

            int score = _scorer.CalculateScore(_board, tiles);

            Assert.Equal(15, score);
        }

        [Fact]
        public void CalculateScore_Intersection_ScoresBothWords()
        {
            _board.PlaceTile(7, 7, new Tile('C', 3));
            _board.PlaceTile(7, 8, new Tile('A', 1));
            _board.PlaceTile(7, 9, new Tile('T', 1));

            var tiles = new List<(int, int, Tile)>
            {
                (8, 9, new Tile('O', 1))
            };

            int score = _scorer.CalculateScore(_board, tiles);

            Assert.Equal(2, score);
        }

        [Fact]
        public void ValidateMove_OccupiedSpace_ReturnsFalse()
        {
            _board.PlaceTile(7, 7, new Tile('C', 3));

            var tiles = new List<(int, int, Tile)>
            {
                (7, 7, new Tile('X', 8))
            };

            bool result = _scorer.ValidateMove(_board, tiles);

            Assert.False(result);
        }

        [Fact]
        public void CalculateScore_Joker_HasZeroValue()
        {
            var tiles = new List<(int, int, Tile)>
            {
                (7, 7, new Tile('C', 3)),
                (7, 8, new Tile('A', 1)),
                (7, 9, new Tile('?', 0, isJoker: true))
            };

            int score = _scorer.CalculateScore(_board, tiles);

            Assert.Equal(4, score);
        }

        [Fact]
        public void CalculateScore_ComplexIntersection_ScoresHorizontalAndVertical()
        {
            _board.PlaceTile(7, 7, new Tile('C', 3));
            _board.PlaceTile(7, 8, new Tile('A', 1));
            _board.PlaceTile(7, 9, new Tile('T', 1));

            _board.PlaceTile(8, 10, new Tile('O', 1));

            var tiles = new List<(int, int, Tile)>
             {
                 (7, 10, new Tile('S', 1))
             };

            int score = _scorer.CalculateScore(_board, tiles);

            Assert.Equal(8, score);
        }

        [Fact]
        public void ValidateMove_InvalidWord_ReturnsFalse()
        {
            _board.PlaceTile(7, 7, new Tile('A', 1));
            _validator.IsValidResult = false;

            var tiles = new List<(int, int, Tile)>
            {
                (7, 8, new Tile('B', 3))
            };

            bool result = _scorer.ValidateMove(_board, tiles);

            Assert.False(result);
        }
    }
}