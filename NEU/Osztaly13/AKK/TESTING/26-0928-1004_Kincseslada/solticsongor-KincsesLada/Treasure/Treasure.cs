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
                if (value <= 0)
                {
                    throw new System.ArgumentException("A térfogat nem lehet 0 vagy negatív.");
                }
                _volume = value;
            }
        }
        public Treasure(string name, int volume)
        {
            Volume = volume;
            Name = name;
        }
    }
}
