namespace kosarligaLib
{
    public class Ligak
    {
        readonly List<Liga> ligak = new();

        public Ligak(IEnumerable<string> sor)
        {
            foreach (var i in sor)
            {
                ligak.Add(new Liga(i));
            }
        }
        public int HazaiMecsek(string bekertCsapat)
        {
            return ligak.Count(x => x.HazaiNev == bekertCsapat);
        }
        public int IdegenMecsek(string bekertCsapat)
        {
            return ligak.Count(x => x.IdegenNev == bekertCsapat);
        }
        public string Dontetlen()
        {
            bool voltDontetlen = ligak.Any(x => x.HazaiPont == x.IdegenPont);
            return voltDontetlen ? "Volt döntetlen mérkőzés" : "Nem volt döntetlen mérkőzés";
        }
        public string Varos(string bekertVaros)
        {
            var csapatok = ligak.Where(x => x.HazaiNev.Contains(bekertVaros)).Select(x => x.HazaiNev).Distinct().ToList();
            return string.Join(",", csapatok);
        }
        public List<string> TobbMintSzaz()
        {
            return ligak.Where(x => x.HazaiPont > 100).OrderBy(x => x.Idopont).Select(x => $"{x.Idopont} - {x.HazaiNev} vs {x.IdegenNev}: {x.HazaiPont}-{x.IdegenPont}").ToList();
        }
        public string ByDate(string bekertIdopont)
        {
            var meccsek = ligak.Where(x => x.Idopont == bekertIdopont).Select(x => $"{x.HazaiNev} vs {x.IdegenNev}: {x.HazaiPont}-{x.IdegenPont}").ToList();
            return string.Join("\n", meccsek);
        }
        public string LegnagyobbKulonbseg()
        {
            var meccs = ligak.OrderByDescending(x => Math.Abs(x.HazaiPont - x.IdegenPont)).First();
            return $"{meccs.Idopont} - {meccs.HazaiNev} vs {meccs.IdegenNev}: {meccs.HazaiPont}-{meccs.IdegenPont} (különbség: {Math.Abs(meccs.HazaiPont - meccs.IdegenPont)})";
        }
        public string Stadionok()
        {
            var stadionok = ligak.GroupBy(x => x.Helyszin).Where(g => g.Count() > 20).Select(g => $"{g.Key}: {g.Count()}").ToList();
            return string.Join("\n\t", stadionok);
        }
    }
}