namespace TreasureChest
{
    public class Treasure
    {
        private int _volume;
        public string Name { get; init; }
        public int Volume
        {
            get => _volume;
            init
            {
                _volume = value;
            }
        }
        public Treasure(string name, int volume)
        {
            if (volume <= 0)
            {
                throw new ArgumentException("A kincs térfogata nem lehet 0 vagy negatív.");
            }
            Volume = volume;
            Name = name;
        }
    }
}
