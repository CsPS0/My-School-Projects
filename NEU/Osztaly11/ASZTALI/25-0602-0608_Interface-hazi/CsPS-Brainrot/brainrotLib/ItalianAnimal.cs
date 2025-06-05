namespace brainrotLib
{
    public class ItalianAnimal : IBrainrot
    {
        public string Nev { get; }
        public string Leiras { get; }
        public DateTime Datum { get; }
        public char Megjelenites => Mix.Length > 0 ? Mix[0] : 'X';
        public string Mix { get; }
        public int Veszelyesseg { get; }

        public ItalianAnimal(string nev, string leiras, DateTime datum, string mix, int veszelyesseg)
        {
            Nev = nev;
            Leiras = leiras;
            Datum = datum;
            Mix = mix;
            Veszelyesseg = veszelyesseg;
        }

        public override string ToString()
        {
            return Leiras;
        }
    }
}