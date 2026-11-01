namespace solticsongor_ScrabbleCLI
{
    public enum BonusType
    {
        None,
        DoubleLetter,
        TripleLetter,
        DoubleWord,
        TripleWord
    }

    public class Tile
    {
        public char Letter { get; set; }
        public int Value { get; set; }
        public bool IsJoker { get; set; }

        public Tile(char letter, int value, bool isJoker = false)
        {
            Letter = letter;
            Value = value;
            IsJoker = isJoker;
        }
    }

    public class Cell
    {
        public BonusType Bonus { get; set; } = BonusType.None;
        public Tile? OccupyingTile { get; set; } = null;
    }

    public interface IWordValidator
    {
        bool IsValid(string word);
    }
}