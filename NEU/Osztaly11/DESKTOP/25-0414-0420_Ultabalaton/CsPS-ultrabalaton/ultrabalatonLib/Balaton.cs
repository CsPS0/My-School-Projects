namespace ultrabalatonLib
{
    public class Balaton
    {
        public string Versenyzo { get; init; }
        public int Rajtszam { get; init; }
        public string Kategoria { get; init; }
        public TimeSpan Versenyido { get; init; }
        public int Tavszazalek { get; init; }

        public Balaton(string adat)
        {
            string[] sor = adat.Split(";");
            Versenyzo = sor[0];
            Rajtszam = int.Parse(sor[1]);
            Kategoria = sor[2];
            Versenyido = IdoFix(sor[3]);
            Tavszazalek = int.Parse(sor[4]);
        }

        public double IdoOraban()
        {
            return Versenyido.TotalHours;
        }

        public static TimeSpan IdoFix(string Ido)
        {
            var bontas = Ido.Split(':');
            if (bontas.Length != 3)
            {
                throw new FormatException("Hibás időformátum");
            }
            int ora = int.Parse(bontas[0]);
            int perc = int.Parse(bontas[1]);
            int masodperc = int.Parse(bontas[2]);
            return new TimeSpan(ora, perc, masodperc);
        }
    }
}