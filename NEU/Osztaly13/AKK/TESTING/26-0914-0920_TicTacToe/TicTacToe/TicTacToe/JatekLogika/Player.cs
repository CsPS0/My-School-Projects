using System;

namespace JatekLogika
{
    /// <summary>
    /// Egy játékost ír le: azonosító, megjelenített név és a hozzá rendelt jel.
    /// </summary>
    public class Player
    {
        public int Id { get; }
        public string Name { get; }
        public Symbol Symbol { get; }

        public Player(int id, string name, Symbol symbol)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A név nem lehet üres.", nameof(name));
            }

            if (symbol == Symbol.None)
            {
                throw new ArgumentException("A játékoshoz X vagy O jelet kell rendelni.", nameof(symbol));
            }

            Id = id;
            Name = name;
            Symbol = symbol;
        }

        /// <summary>
        /// Egy "id;nev" formátumú CSV sorból hoz létre játékost.
        /// </summary>
        public static Player FromCsvLine(string line, Symbol symbol)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new FormatException("Az üres sor nem alakítható játékossá.");
            }

            string[] fields = line.Split(';');
            if (fields.Length < 2)
            {
                throw new FormatException($"A sornak 'id;nev' formátumúnak kell lennie: '{line}'.");
            }

            if (!int.TryParse(fields[0], out int id))
            {
                throw new FormatException($"Az azonosító nem szám: '{fields[0]}'.");
            }

            return new Player(id, fields[1].Trim(), symbol);
        }

        public override string ToString() => $"{Name} ({Symbol})";
    }
}
