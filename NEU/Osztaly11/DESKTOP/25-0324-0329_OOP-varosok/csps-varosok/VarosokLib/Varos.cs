namespace VarosokLib
{
    public class Varos
    {
        public string Nev { get; set; }
        public string Varmegye { get; set; }
        public int Nepesseg { get; set; }
        public double Terulet { get; set; }
        public int? Alapitas { get; set; }
        public double Nepsuruseg { get; set; }

        public Varos(string adat)
        {
            string[] sor = adat.Split(';');
            Nev = sor[0];
            Varmegye = sor[1];
            Nepesseg = int.Parse(sor[2]);
            Terulet = double.Parse(sor[3]);
            if (sor[4] == "1885 előtt")
            {
                Alapitas = null;
            }
            else
            {
                Alapitas = int.Parse(sor[4]);
            }
            Nepsuruseg = Nepesseg / Terulet;
        }
    }
}