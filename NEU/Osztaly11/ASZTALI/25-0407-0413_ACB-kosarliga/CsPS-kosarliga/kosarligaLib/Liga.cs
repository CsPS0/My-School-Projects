namespace kosarligaLib
{
    public class Liga
    {
        public string HazaiNev { get; init; }
        public string IdegenNev { get; init; }
        public int HazaiPont { get; init; }
        public int IdegenPont { get; init; }
        public string Helyszin { get; init; }
        public string Idopont { get; init; }

        public Liga(string adat)
        {
            string[] sor = adat.Split(";");
            HazaiNev = sor[0];
            IdegenNev = sor[1];
            HazaiPont = int.Parse(sor[2]);
            IdegenPont = int.Parse(sor[3]);
            Helyszin = sor[4];
            Idopont = sor[5];
        }
    }
}