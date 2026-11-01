namespace joalanyLib
{
    public sealed class Tanar : Szemely, IVizsgalat
    {
        public double JegyekAtlaga { get; set; }
        
        public Tanar(string nev, DateTime szuletesiDatum, double jegyekAtlaga) : base(nev, szuletesiDatum)
        {
            JegyekAtlaga = jegyekAtlaga;
        }

        public bool JoAlanyE()
        {
            return Kor < 30 && JegyekAtlaga >= 3.5;
        }

        public override string ToString()
        {
            return base.ToString() + $", jegyek átlaga: {JegyekAtlaga:F2}" + (JoAlanyE() ? ", jó alany" : ", nem jó alany");
        }
    }
}