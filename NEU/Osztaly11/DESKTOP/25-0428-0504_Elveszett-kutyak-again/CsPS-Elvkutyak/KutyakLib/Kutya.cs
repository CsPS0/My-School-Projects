namespace KutyakLib
{
    public class Kutya
    {
        public string Nev { get; init; }
        public string Nem { get; init; }
        public string Faj { get; init; }
        public string Hely { get; init; }
        public DateTime Mikor { get; init; }

        public Kutya(string adat)
        {
            string[] sor = adat.Split(";");
            Nev = sor[0];
            Nem = sor[1];
            Faj = sor[2];
            Hely = sor[3];
            Mikor = DateTime.Parse(sor[4]);
        }
    }
}
