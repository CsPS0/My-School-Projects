namespace dartsLib
{
    public class Darts
    {
        readonly List<Dart> darts = new();

        public Darts(IEnumerable<string> sor)
        {
            foreach (var i in sor)
            {
                darts.Add(new Dart(i));
            }
        }

        public int Ossz => darts.Count;
        public int Bullseye => darts.Count(x => x.Harmadik == "D25");
        public string Szektor(string bekertSzektor)
        {
            var szektorok = darts.Where(x => x.Elso.Contains(bekertSzektor) || x.Masodik.Contains(bekertSzektor) || x.Harmadik.Contains(bekertSzektor)).GroupBy(y => y.Jatekos).Select(z => new { Jatekos = z.Key, Count = z.Count() }).ToList();
            string kiiras = "";
            foreach (var i in szektorok)
            {
                kiiras += $"Az {i.Jatekos}. játékos a(z) {bekertSzektor} szektoros dobásainak száma: {i.Count}";
            }
            return kiiras;
        }

        public string Szektor2(string bekertSzektor)
        {
            var szektorok = darts
                .GroupBy(x => x.Jatekos)
                .Select(g => new
                {
                    Jatekos = g.Key,
                    Count = g.Count(x => x.Elso.Contains(bekertSzektor) || x.Masodik.Contains(bekertSzektor) || x.Harmadik.Contains(bekertSzektor))
                })
                .ToList();

            string kiiras = "";
            foreach (var i in szektorok)
            {
                kiiras += $"Az {i.Jatekos}. játékos a(z) {bekertSzektor} szektoros dobásainak száma: {i.Count}\n";
            }
            return kiiras;
        }

        public string Legmagasabb()
        {
            var jatekosok = darts.Where(d => d.Elso == "T20" && d.Masodik == "T20" && d.Harmadik == "T20").GroupBy(d => d.Jatekos).Select(g => new { Jatekos = g.Key, Count = g.Count() }).ToList();

            string kiiras = "";
            foreach (var i in jatekosok)
            {
                kiiras += $"Az {i.Jatekos}. játékos {i.Count} db 180-ast dobott.\n";
            }

            return kiiras;
        }

    }
}