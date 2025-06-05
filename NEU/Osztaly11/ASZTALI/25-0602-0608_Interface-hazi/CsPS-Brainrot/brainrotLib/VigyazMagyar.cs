using System.Text;

namespace brainrotLib
{
    public class VigyazMagyar : IBrainrot
    {
        static VigyazMagyar()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public string Nev { get; }
        public string Leiras { get; }
        public DateTime Datum { get; }
        public char Megjelenites { get; }
        public bool PolitikaE { get; }
        public char Offenziv { get; }

        public VigyazMagyar(string nev, string leiras, DateTime datum, char megjelenites, bool politikaE, char offenziv)
        {
            Nev = nev;
            Leiras = leiras;
            Datum = datum;
            Megjelenites = megjelenites;
            PolitikaE = politikaE;
            Offenziv = offenziv;
        }

        public override string ToString()
        {
            return $"{Nev}: {Leiras} (Politika: {(PolitikaE ? "igen" : "nem")}, Offenzív: {Offenziv})";
        }
    }
}