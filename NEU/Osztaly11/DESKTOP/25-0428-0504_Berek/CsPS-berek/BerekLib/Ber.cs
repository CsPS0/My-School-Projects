namespace BerekLib
{
    public class Ber
    {
        public string Nev { get; init; }
        public string Neme { get; init; }
        public string Reszleg { get; init; }
        public int Belepesev { get; init; }
        public int BerFt { get; init; }

        public Ber(string sor)
        {
            string[] v = sor.Split(";");
            Nev = v[0];
            Neme = v[1];
            Reszleg = v[2];
            Belepesev = int.Parse(v[3]);
            BerFt = int.Parse(v[4]);
        }

        public string Keresztnevek()
        {
            var nevek = Nev.Split(' ').Skip(1);
            return string.Join(",", nevek);
        }
    }
}