namespace brainrotLib
{
    public class SkibidiToilet : IComparable<SkibidiToilet>, IBrainrot
    {
        public string Nev { get; }
        public string Leiras { get; }
        public string Ritkasag { get; }
        public int Ismertseg { get; }
        public char Megjelenites { get; }
        public SkibidiToilet(string nev, string leiras, string ritkasag, int ismerteg, char megjelenites)
        {
            Nev = nev;
            Leiras = leiras;
            Ritkasag = ritkasag;
            Ismertseg = ismerteg;
            Megjelenites = megjelenites;
        }
        public override string ToString()
        {
            return Leiras;
        }
        public int CompareTo(SkibidiToilet? other)
        {
            if (other == null) return 1;
            return Ismertseg.CompareTo(other.Ismertseg);
        }
    }
}