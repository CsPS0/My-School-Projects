namespace joalanyLib
{
    public sealed class Diak : Szemely, IVizsgalat
    {
        public int PuskakSzama { get; set; }

        public Diak(string nev, DateTime szuletesiDatum, int puskakSzama)
            : base(nev, szuletesiDatum)
        {
            PuskakSzama = puskakSzama;
        }

        public bool JoAlanyE()
        {
            return PuskakSzama == 0;
        }

        public override string ToString()
        {
            return base.ToString() + (JoAlanyE() ? ", jó alany" : ", nem jó alany");
        }
    }
}