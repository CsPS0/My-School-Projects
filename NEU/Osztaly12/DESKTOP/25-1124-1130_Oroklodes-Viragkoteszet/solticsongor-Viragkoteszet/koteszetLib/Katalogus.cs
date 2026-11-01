namespace koteszetLib
{
    public class Katalogus
    {
        public Dictionary<string, Alapanyag> alapanyagok;

        public Katalogus(IEnumerable<Alapanyag> alapanyagSorozat)
        {
            alapanyagok = alapanyagSorozat.ToDictionary(a => a.Azonosito);
        }

        public Alapanyag this[string azonosito]
        {
            get
            {
                if (alapanyagok.ContainsKey(azonosito))
                {
                    return alapanyagok[azonosito];
                }
                else
                {
                    throw new KeyNotFoundException($"Nem található: {azonosito}");
                }
            }
        }
    }
}