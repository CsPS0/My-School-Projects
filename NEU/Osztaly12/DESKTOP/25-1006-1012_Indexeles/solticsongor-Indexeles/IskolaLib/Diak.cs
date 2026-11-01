namespace IskolaLib
{
    public class Diak
    {
        public string Nev { get; set; }
        public string Azon { get; set; }
        public Dictionary<Tantargy, int> Jegyek { get; set; }

        public Diak(string nev, string azon)
        {
            Nev = nev;
            Azon = azon;
            Jegyek = new Dictionary<Tantargy, int>();
        }

        public void UjJegy(Tantargy tantargy, int jegy)
        {
            Jegyek[tantargy] = jegy;
        }
    }
}