namespace csokigyarLib
{
    public class Csoki : IEtel
    {
        protected string Csokifajta { get; init; }
        protected string[] Alapanyagok { get; init; }
        protected int KakoTartalom { get; init; }

        public Csoki(string csokifajta, IEnumerable<string> alapanyagok, int kakoTartalom)
        {
            Csokifajta = csokifajta;
            Alapanyagok = alapanyagok.ToArray();
            KakoTartalom = kakoTartalom;
        }

        public IEnumerable<string> MibolKeszul()
        {
            return Alapanyagok;
        }

        public bool MegfeleloMinosegu => KakoTartalom switch
        {
            > 50 => true,
            >= 0 => false,
            _ => throw new SilanyMinosegException()
        };

        public override string ToString()
        {
            return $"{Csokifajta} kakaótartalom: {KakoTartalom}% alapanyagai: {String.Join(", ", Alapanyagok)}";
        }
    }
}