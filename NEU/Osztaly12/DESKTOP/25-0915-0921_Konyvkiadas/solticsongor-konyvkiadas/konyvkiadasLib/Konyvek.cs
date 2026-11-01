namespace konyvkiadasLib
{
    public class Konyvek
    {
        readonly List<Konyv> konyvek = new();

        public Konyvek(IEnumerable<string> sorok)
        {
            foreach (var i in sorok)
            {
                konyvek.Add(new Konyv(i));
            }
        }

        public int BekeresSzerzoAlkalom(string szerzo)
        {
            return konyvek
                .Count(x => x.Leiras.Contains(szerzo));
        }

        public string LegnagyobbPeldanySzam()
        {
            var group = konyvek
                .GroupBy(x => x.Peldanyszam)
                .OrderByDescending(g => g.Key)
                .First();
            return $"Legnagyobb példányszám: {group.Key}, előfordult {group.Count()} alkalommal.";
        }

        public string Legalabb40KPeldany()
        {
            var konyv = konyvek
                .First(x => x.Eredet == "kf" && x.Peldanyszam >= 40000);
            return $"{konyv.Ev}/{konyv.NegyedEv}. {konyv.Leiras}";
        }

        public string LegalabbKetszerNagyobbPeldany()
        {
            return string.Join("\n", konyvek
                .GroupBy(k => k.Leiras)
                .Where(g => g.Count() > 1 && g.Any(k => k.Peldanyszam > g.OrderBy(k_inner => k_inner.Ev).ThenBy(k_inner => k_inner.NegyedEv).First().Peldanyszam))
                .Select(g => g.Key));
        }
    }
}