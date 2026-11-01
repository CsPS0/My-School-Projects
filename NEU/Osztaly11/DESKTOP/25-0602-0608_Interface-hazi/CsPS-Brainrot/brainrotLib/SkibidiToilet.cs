namespace brainrotLib
{
    public class SkibidiToilet : IBrainrot
    {
        private string _nev;
        public string Nev => _nev + " Skibidi Toilet";
        public string Leiras { get; }
        public DateTime Datum { get; }
        public char Megjelenites { get; }
        public string Gyakorisag { get; }

        public SkibidiToilet(string nev, string leiras, DateTime datum, char megjelenites, string gyakorisag)
        {
            _nev = nev;
            Leiras = leiras;
            Datum = datum;
            Megjelenites = megjelenites;
            Gyakorisag = gyakorisag;
        }

        public override string ToString()
        {
            return Leiras;
        }
    }
}