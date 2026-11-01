namespace csokigyarLib
{
    public class EtelFactory
    {
        public IEtel Factory(string adatSor)
        {
            string[] adatok = adatSor.Split(';');
            if (adatok[^1] == "prémium")
            {
                return (IEtel)new PremiumCsoki(
                    adatok[0],
                    adatok[2..^1],
                    int.Parse(adatok[1])
                );
            }
            return new Csoki(
                adatok[0],
                adatok[2..],
                int.Parse(adatok[1])
            );
        }
    }
}