namespace VarosokLib
{
    public class Varosok
    {
        readonly List<Varos> varosLista = new List<Varos>();

        public Varosok(IEnumerable<string> sorok)
        {
            foreach (var sor in sorok)
                varosLista.Add(new Varos(sor));
        }

        public int HanyVaros => varosLista.Count;
        public Varos? Keves2K => varosLista.FirstOrDefault(x => x.Nepesseg < 2000);
        public double TeruletAVG => Math.Round(varosLista.Average(x => x.Terulet), 2);
        public int Rendszervaltas => varosLista.Count(x => x.Alapitas.HasValue && x.Alapitas > 1990);
        public Varos MinNepsuruseg => varosLista.OrderBy(x => x.Nepsuruseg).First();
        public bool VoltVarossa(int ev) => varosLista.Any(x => x.Alapitas == ev);
        public List<Varos> LegsurubbPest => varosLista.Where(x => x.Varmegye == "Pest").OrderByDescending(x => x.Nepsuruseg).Take(3).ToList();
        public string NogradVarosok => string.Join(", ", varosLista.Where(x => x.Varmegye == "Nógrád").OrderBy(x => x.Nev).Select(x => x.Nev));
        public Dictionary<string, int> Megyenkent => varosLista.GroupBy(x => x.Varmegye).OrderBy(x => x.Key).ToDictionary(g => g.Key, g => g.Count());
    }
}