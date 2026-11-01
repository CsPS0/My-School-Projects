namespace BerekLib
{
    public class Berek
    {
        readonly List<Ber> berek = new();

        public Berek(IEnumerable<string> sorok)
        {
            foreach (var s in sorok)
            {
                berek.Add(new Ber(s));
            }
        }

        public int Osszes => berek.Count;
        public double Atlagber => Math.Round(berek.Average(x => x.BerFt) / 1000, 1);
        public string Reszlegek => string.Join(", ", berek.Select(x => x.Reszleg).Distinct().OrderBy(x => x));
        public Ber? LegtobbetKereso(string bekeres)
        {
            if (!berek.Any(x => x.Reszleg == bekeres))
                return null;

            return berek.Where(x => x.Reszleg == bekeres).OrderByDescending(x => x.BerFt).First();
        }
        public string[] ReszlegenkentDolgozokSzama()
        {
            return berek.GroupBy(x => x.Reszleg).Select(g => $"{g.Key} - {g.Count()} fő").ToArray();
        }
        public string[] LegkisebbBeruek()
        {
            return berek.GroupBy(x => x.Reszleg)
                        .Select(g => {
                            var dolgozo = g.OrderBy(x => x.BerFt).First();
                            return $"{g.Key}: {dolgozo.Nev} ({dolgozo.BerFt} Ft)";
                        })
                        .ToArray();
        }
        public string KeresztnevekNemenként(string nem)
        {
            return string.Join(", ", berek.Where(x => x.Neme == nem).SelectMany(x => x.Nev.Split(' ').Skip(1)).Distinct().OrderBy(x => x));
        }
        public string[] ReszlegNemStatisztika()
        {
            return berek.GroupBy(x => x.Reszleg).Select(g => $"{g.Key}: nő - {g.Count(x => x.Neme == "nő")} fő, férfi - {g.Count(x => x.Neme == "férfi")} fő").ToArray();
        }
        public string[] AtlagBerReszlegenkent()
        {
            return berek.GroupBy(x => x.Reszleg).Select(g => $"{g.Key}: {g.Average(x => x.BerFt):F2} Ft").ToArray();
        }
        public Dictionary<int, int> Jubileum()
        {
            return berek.GroupBy(x => 2020 - x.Belepesev).Where(x => x.Key % 5 == 0 && x.Key > 0).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Count());
        }
    }
}