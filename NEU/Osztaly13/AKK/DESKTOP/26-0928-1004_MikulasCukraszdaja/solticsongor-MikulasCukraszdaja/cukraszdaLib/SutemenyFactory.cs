namespace cukraszdaLib
{
    public class SutemenyFactory
    {
        public static Sutemeny Factory(string sor, KeszitesAdatok keszitesAdatok)
        {
            string[] adatok = sor.Split(';');
            string azonosito = adatok[0];
            string tipus = adatok[1];
            string megnevezes = adatok[2];

            if (tipus == "f")
            {
                var osszetevok = adatok.Skip(3).Where(s => !string.IsNullOrWhiteSpace(s));
                return new DiszitettSutemeny(azonosito, tipus, megnevezes, keszitesAdatok, osszetevok);
            }

            return new AlapSutemeny(azonosito, tipus, megnevezes, keszitesAdatok);
        }
    }
}
