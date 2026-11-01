namespace novenygyujtesLib
{
    public class Gyogynoveny : IComparable<Gyogynoveny>, INoveny
    {
        public string Nev { get; }
        public string Leiras { get; }
        public string Tipus { get; }
        public int Ertek { get; }
        public char Megjelenites { get; }
        string hatasa { get; }
        public string Hatas()
        {
            return hatasa;
        }
        public Gyogynoveny(string nev, string leiras, string tipus, int ertek, char megjelenites, string hatasa)
        {
            Nev = nev;
            Leiras = leiras;
            Tipus = tipus;
            Ertek = ertek;
            Megjelenites = megjelenites;
            this.hatasa = hatasa;
        }
        public override string ToString()
        {
            return Leiras + " - Gógyhatása: " + hatasa;
        }
        public int CompareTo(Gyogynoveny? other)
        {
            if (other == null) return 1;
            return Ertek.CompareTo(other.Ertek);
        }
    }
}
