namespace novenygyujtesLib
{
    public class Virag : IComparable<Virag>, INoveny
    {
        public string Nev { get; }
        public string Leiras { get; }
        public string Tipus { get; }
        public int Ertek { get; }
        public char Megjelenites { get; }
        public Virag(string nev, string leiras, string tipus, int ertek, char megjelenites)
        {
            Nev = nev;
            Leiras = leiras;
            Tipus = tipus;
            Ertek = ertek;
            Megjelenites = megjelenites;
        }
        public override string ToString()
        {
            return Leiras + ".";
        }
        public int CompareTo(Virag? other)
        {
            if (other == null) return 1;
            return Ertek.CompareTo(other.Ertek);
        }
    }
}
