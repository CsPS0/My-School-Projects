namespace ultrabalatonLib
{
    public class Balatonok
    {
        readonly List<Balaton> balatonok = new();

        public Balatonok(IEnumerable<string> sor)
        {
            foreach (var i in sor)
            {
                balatonok.Add(new Balaton(i));
            }
        }

        public int Ossz => balatonok.Count;

        public int Noi => balatonok.Count(x => x.Tavszazalek == 100 && x.Kategoria == "Noi");

        public bool Indult(string bekertNev)
        {
            return balatonok.Any(x => x.Versenyzo == bekertNev);
        }

        public bool Teljesitette(string bekertNev)
        {
            var versenyzo = balatonok.FirstOrDefault(x => x.Versenyzo == bekertNev);
            return versenyzo != null && versenyzo.Tavszazalek == 100;
        }

        public (string nev, int rajtszam, TimeSpan ido) NoiGyoztes()
        {
            var noiGyoztes = balatonok.Where(x => x.Kategoria == "Noi" && x.Tavszazalek == 100).OrderBy(x => x.Versenyido).FirstOrDefault();

            if (noiGyoztes != null)
            {
                return (noiGyoztes.Versenyzo, noiGyoztes.Rajtszam, noiGyoztes.Versenyido);
            }
            return (null, 0, TimeSpan.Zero);
        }

        public (string nev, int rajtszam, TimeSpan ido) FerfiGyoztes()
        {
            var ferfiGyoztes = balatonok.Where(x => x.Kategoria == "Ferfi" && x.Tavszazalek == 100).OrderBy(x => x.Versenyido).FirstOrDefault();

            if (ferfiGyoztes != null)
            {
                return (ferfiGyoztes.Versenyzo, ferfiGyoztes.Rajtszam, ferfiGyoztes.Versenyido);
            }
            return (null, 0, TimeSpan.Zero);
        }

        public double FerfiAtlagido()
        {
            var ferfiak = balatonok.Where(x => x.Kategoria == "Ferfi" && x.Tavszazalek == 100).Select(x => x.IdoOraban());
            return ferfiak.Average();
        }
    }
}
