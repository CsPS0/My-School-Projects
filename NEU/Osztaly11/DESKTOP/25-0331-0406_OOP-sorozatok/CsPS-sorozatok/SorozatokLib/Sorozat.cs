using System.Globalization;

namespace SorozatokLib
{
    public class Sorozat
    {
        public string Cim {  get; init; }
        public string Rendezo { get; init; }
        public string Szarmazas { get; init; }
        public int Ev {  get; init; }
        public string Mufaj { get; init; }
        public int Evadok {  get; init; }

        public Sorozat(string adat)
        {
            string[] sor = adat.Split(";");
            Cim = sor[0];
            Rendezo = sor[1];
            Szarmazas = sor[2];
            Ev = int.Parse(sor[3]);
            Mufaj = sor[4];
            Evadok = int.Parse(sor[5]);
        }
        public int Rendezok()
        {
            var rend = Rendezo.Split(",").Count();
            return rend;
        }
    }
}