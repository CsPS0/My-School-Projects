namespace brainrotLib
{
    public class ItalianAnimal : IComparable<ItalianAnimal>, IBrainrot
    {
        public string Nev { get; }
        public string Leiras { get; }
        public string Ritkasag { get; }
        public int Ismertseg { get; }
        public char Megjelenites { get; }
        string veszelyesseg { get; }
        public string Veszelyesseg()
        {
            return veszelyesseg;
        }
        public ItalianAnimal(string nev, string leiras, string ritkasag, int ismerteg, char megjelenites)
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
        public int CompareTo(ItalianAnimal? other)
        {
            if (other == null) return 1;
            return Ismertseg.CompareTo(other.Ismertseg);
        }
    }
}