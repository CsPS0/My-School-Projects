namespace szeleromuvekLib
{
    public class Szeleromuvek
    {
        readonly List<Szeleromu> szeleromuvek = new();

        public Szeleromuvek(IEnumerable<string> sorok)
        {
            foreach (var i in sorok)
            {
                szeleromuvek.Add(new Szeleromu(i));
            }
        }

        public int Osszes => szeleromuvek.Count;

        public string[] TelepitesekSzamaRegonkent()
        {
            return szeleromuvek.GroupBy(x => x.Regio)
                .Select(g => $"{g.Key} - {g.Count()} telepítés")
                .ToArray();
        }

        public string LegtobbTelepitesRegio()
        {
            var regio = szeleromuvek.GroupBy(sz => sz.Regio)
                .OrderByDescending(g => g.Count())
                .First();

            return $"{regio.Key} régióban volt a legtöbbször szélerőmű telepítés ({regio.Count()} alkalommal)";
        }

        public string[] SzeleromuvekSzamaRegonkent()
        {
            return szeleromuvek.GroupBy(sz => sz.Regio)
                .Select(g => $"{g.Key}: {g.Sum(sz => sz.Darab)} darab")
                .ToArray();
        }

        public string[] SzeleromuvekSzamaMegyeenkent()
        {
            return szeleromuvek.GroupBy(sz => sz.Regio)
                .Select(g => $"{g.Key}: " + string.Join(", ", g.GroupBy(mg => mg.Megye).Select(mg => $"{mg.Key} - {mg.Sum(sz => sz.Darab)} darab")))
                .ToArray();
        }

        public string[] SzeleromuvekSzamaTelepulesenkent()
        {
            return szeleromuvek.GroupBy(sz => sz.Telepules)
                .Select(g => $"{g.Key}: {g.Sum(sz => sz.Darab)} darab")
                .ToArray();
        }

        public string[] LegtobbSzeleromuTelepules(int n)
        {
            return szeleromuvek.GroupBy(sz => sz.Telepules)
                .Select(g => new { Telepules = g.Key, Darab = g.Sum(sz => sz.Darab) })
                .OrderByDescending(x => x.Darab)
                .Take(n)
                .Select(x => $"{x.Telepules}: {x.Darab} darab")
                .ToArray();
        }

        public string[] AtlagosTeljesitmenyTelepulesenkent()
        {
            return szeleromuvek.GroupBy(sz => sz.Telepules)
                .Select(g => $"{g.Key}: {g.Average(sz => sz.Teljesitmeny):F2} kW")
                .ToArray();
        }

        public string[] LegnagyobbOsszteljesitmenyuTelepulesek(int n)
        {
            return szeleromuvek.GroupBy(sz => sz.Telepules)
                .Select(g => new
                {
                    Telepules = g.Key,
                    OsszTeljesitmeny = g.Sum(sz => sz.Teljesitmeny * sz.Darab)
                })
                .OrderByDescending(x => x.OsszTeljesitmeny)
                .Take(n)
                .Select(x => $"{x.Telepules} településen az összteljesítmény: {x.OsszTeljesitmeny:F0} kW")
                .ToArray();
        }
    }
}