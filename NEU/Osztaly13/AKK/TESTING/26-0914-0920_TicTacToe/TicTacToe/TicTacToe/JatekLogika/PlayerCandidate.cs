namespace JatekLogika
{
    /// <summary>
    /// Egy a players.csv-ből beolvasott sor: azonosító és név, jel hozzárendelés nélkül.
    /// A kiválasztott két jelöltből lesz a végleges <see cref="Player"/>, amikor
    /// a megjelenítő réteg X-et vagy O-t rendel hozzájuk.
    /// </summary>
    public class PlayerCandidate
    {
        public int Id { get; }
        public string Name { get; }

        public PlayerCandidate(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Player ToPlayer(Symbol symbol) => new Player(Id, Name, symbol);

        public override string ToString() => $"{Id}: {Name}";
    }
}
