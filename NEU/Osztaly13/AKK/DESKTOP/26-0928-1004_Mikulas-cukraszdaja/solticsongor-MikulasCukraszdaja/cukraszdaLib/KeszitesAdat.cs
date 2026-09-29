namespace cukraszdaLib
{
    public class KeszitesAdat
    {
        public string Azonosito { get; init; }
        public string Tipus { get; init; }
        public int ElkeszitesiIdo { get; init; }

        public KeszitesAdat(string adat)
        {
            string[] adatok = adat.Split(';');
            Azonosito = adatok[0];
            Tipus = adatok[1];
            ElkeszitesiIdo = int.Parse(adatok[2]);
        }
    }
}
