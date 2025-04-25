namespace Hegyek_lib
{
    public class Hegyek
    {
        readonly List<Hegy> hegyek = new List<Hegy>();
        public Hegyek(IEnumerable<string> fajlSorok)
        {
            foreach (var fajl in fajlSorok)
            {
                hegyek.Add(new Hegy(fajl));
            }
        }

        public int HegyAdatMennyiseg => hegyek.Count;
        public Hegy AlacsonyHegy => hegyek.MinBy(x => x.Magassag);
        public double AtlagMagassag => hegyek.Average(x =>x.Magassag);
        public int MatraCsucsok => hegyek.Where(x => x.Hegyseg == "Mátra").Count();
        public int BercCsucsok => hegyek.Where(x => x.Nev.Contains("bérc")).Count();
        public List<string> HegysegekABC => hegyek.Select(h => h.Hegyseg).Distinct().OrderBy(h => h).ToList();
        public int HaromezerLabnalMagasabb => hegyek.Count(h => h.MagassagFoot > 3000);
        public List<Hegy> HaromLegmagasabb => hegyek.OrderByDescending(h => h.Magassag).Take(3).ToList();
    }
}