namespace konyvkiadasLib
{
    public class Konyv
    {
        public int Ev {  get; init; }
        public int NegyedEv { get; init; }
        public string Eredet { get; init; }
        public string Leiras { get; init; }
        public int Peldanyszam { get; init; }

        public Konyv(string sor)
        {
            string[] s = sor.Split(";");
            Ev = int.Parse(s[0]);
            NegyedEv = int.Parse(s[1]);
            Eredet = s[2];
            Leiras = s[3];
            Peldanyszam = int.Parse(s[4]);
        }
    }
}
