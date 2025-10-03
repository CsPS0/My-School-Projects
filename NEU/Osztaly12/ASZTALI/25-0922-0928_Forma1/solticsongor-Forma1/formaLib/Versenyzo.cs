namespace formaLib
{
    public class Versenyzo
    {
        public DateOnly Datum { get; init; }
        public string Helyszin { get; init; }
        public string Nev { get; init; }
        public string Nem { get; init; }
        public DateOnly? Szuldat { get; init; }
        public string Nemzet { get; init; }
        public int? Helyezes { get; init; }
        public string Hiba { get; init; }
        public string Csapat { get; init; }
        public string Tipus { get; init; }
        public string Motor { get; init; }

        public Versenyzo(string sor)
        {
            string[] adatok = sor.Split(';');
            Datum = DateOnly.Parse(adatok[0]);
            Helyszin = adatok[1];
            Nev = adatok[2];
            Nem = adatok[3];
            Szuldat = string.IsNullOrEmpty(adatok[4]) ? null : DateOnly.Parse(adatok[4]);
            Nemzet = adatok[5];
            Helyezes = string.IsNullOrEmpty(adatok[6]) ? null : int.Parse(adatok[6]);
            Hiba = adatok[7];
            Csapat = adatok[8];
            Tipus = adatok[9];
            Motor = adatok[10];
        }
    }
}