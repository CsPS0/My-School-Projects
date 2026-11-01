namespace novenygyujtesLib
{
    public class Gomba : IComparable<Gomba>, INoveny
    {
        public string Nev { get; }
        public string Leiras { get; }
        public string Tipus { get; }
        public int Ertek { get; }
        public char Megjelenites { get; }
        public bool Mergezo { get; }
        public Gomba(string nev, string leiras, string tipus, int ertek, char megjelenites, bool mergezo)
        {
            Nev = nev;
            Leiras = leiras;
            Tipus = tipus;
            Ertek = ertek;
            Megjelenites = megjelenites;
            Mergezo = mergezo;
        }
        public override string ToString()
        {
            return Leiras + (Mergezo ? " (mérgező)" : "");
        }
        public int CompareTo(Gomba? other)
        {
            if (other == null) return 1;
            return Ertek.CompareTo(other.Ertek);
        }
    }
}
