namespace csokigyarLib
{
    public class PremiumCsoki : Csoki
    {
        public PremiumCsoki(string csokifajta, IEnumerable<string> alapanyagok, int kakaoTartalom) : base(csokifajta, alapanyagok, kakaoTartalom)
        {
            // ha nem megy, hát nem megy
        }

        public override string ToString()
        {
            return $"Prémium {base.ToString()}";
        }
    }
}