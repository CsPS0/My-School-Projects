namespace brainrotLib
{
    public class VigyazMagyar : IComparable<VigyazMagyar>, IBrainrot
    {
        public string Nev { get; }
        public string Leiras { get; }
        public string Ritkasag { get; }
        public int Ismertseg { get; }
        public char Megjelenites { get; }
        string offenziv { get; }
        public string Veszelyesseg()
        {
            return offenziv;
        }
        public VigyazMagyar(string nev, string leiras, string ritkasag, int ismerteg, char megjelenites)
        {
            Nev = nev;
            Leiras = leiras;
            Ritkasag = ritkasag;
            Ismertseg = ismerteg;
            Megjelenites = megjelenites;
        }
        public int CompareTo(VigyazMagyar? other)
        {
            if (other == null) return 1;
            return Ismertseg.CompareTo(other.Ismertseg);
        }
    }
}