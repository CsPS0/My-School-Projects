namespace nullableLib
{
    public class Tanulo
    {
        public string Nev { get; set; }
        public double Szazalek { get; private set; }
        public string Eredmeny { get; private set; }
        public int?[] Pontok { get; private set; } = new int?[5];
        public int Osszpontszam { get; private set; }

        public Tanulo(string sor)
        {
            string[] adatok = sor.Split(';');
            Nev = adatok[0];
            int osszpontszam = 0;
            for (int i = 0; i < 5; i++)
            {
                if (adatok.Length > i + 1 && !string.IsNullOrEmpty(adatok[i + 1]))
                {
                    int pont = int.Parse(adatok[i + 1]);
                    Pontok[i] = pont;
                    osszpontszam += pont;
                }
                else
                {
                    Pontok[i] = null;
                }
            }
            Osszpontszam = osszpontszam;
            Szazalek = (double)Osszpontszam / 25 * 100;
            Eredmeny = Szazalek >= 40 
                ? "sikeres" 
                : "sikertelen";
        }
    }
}
