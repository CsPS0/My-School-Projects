namespace dartsLib
{
    public class Dart
    {
        public int Jatekos { get; init; }
        public string Elso { get; init; }
        public string Masodik { get; init; }
        public string Harmadik { get; init; }

        public Dart(string adat)
        {
            string[] sor = adat.Split(';');
            Jatekos = int.Parse(sor[0]);
            Elso = sor[1];
            Masodik = sor[2];
            Harmadik = sor[3];
        }
    }
}