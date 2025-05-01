namespace KutyakLib
{
    public class Kutyak
    {
        readonly List<Kutya> kutyak = new();

        public Kutyak(IEnumerable<string> sor)
        {
            foreach (var i in sor)
            {
                kutyak.Add(new Kutya(i));
            }
        }

        public int Ossz => kutyak.Count;

        public string Elveszettek()
        {
            return string.Join("\n\t", kutyak.Where(k => k.Mikor == new DateTime(2020, 12, 31)).Select(k => $"{k.Nev};{(k.Nem == "" ? "n.a." : k.Nem)};{k.Faj};{k.Hely}"));
        }

        public int Ev2020 => kutyak.Count(k => k.Mikor.Year == 2020);
        public int Ev2021 => kutyak.Count(k => k.Mikor.Year == 2021);
        public string FajtakNevsor => string.Join("; ", kutyak.Select(k => k.Faj).Distinct().OrderBy(f => f));
        public string TobbszorosNevek()
        {
            return string.Join("\n\t", kutyak.GroupBy(k => k.Nev).Where(g => g.Count() > 1).Select(g => $"{g.Key}: {g.Count()}"));
        }

        public int SzukakSzama => kutyak.Count(k => k.Nem == "szuka");
        public int KanokSzama => kutyak.Count(k => k.Nem == "kan");
        public int IsmeretlenNem => kutyak.Count(k => k.Nem == "");

        public string KeruletiStatisztika()
        {
            return string.Join("\n\t", kutyak.Where(k => k.Hely.ToLower().Contains("kerület")).GroupBy(k => k.Hely).OrderBy(g => g.Key).Select(g => $"{g.Key}: {g.Count()}"));
        }

        public string FajtakLegalabb5Kutya()
        {
            return string.Join("\n\t", kutyak.GroupBy(k => k.Faj).Where(g => g.Count() >= 5).Select(g => g.Key));
        }

        public string HaviEltunesek()
        {
            return string.Join("\n", kutyak.GroupBy(k => k.Mikor.ToString("yyyy-MM")).OrderBy(g => g.Key).Select(g => $"{g.Key}: {g.Count()}"));
        }

        public string HelyNemStatisztika()
        {
            return string.Join("\n\t", kutyak.GroupBy(k => k.Hely).OrderBy(g => g.Key).Select(g =>
                {
                    var szuka = g.Count(x => x.Nem == "szuka");
                    var kan = g.Count(x => x.Nem == "kan");
                    var na = g.Count(x => x.Nem == "");
                    return $"{g.Key}: kan: {kan}, szuka: {szuka}, n.a.: {na}";
                }));
        }

        public string FajtaHelyek()
        {
            return string.Join("\n\t", kutyak.GroupBy(k => k.Faj).Select(g =>
                {
                    var helyek = string.Join(", ", g.Select(k => k.Hely).Distinct().OrderBy(h => h));
                    return $"{g.Key}: {helyek}";
                }));
        }

        public string NevFajtak()
        {
            return string.Join("\n\t", kutyak.GroupBy(k => k.Nev).Select(g =>
                {
                    var fajtak = string.Join(", ", g.Select(k => k.Faj).Distinct().OrderBy(f => f));
                    return $"{g.Key}: {fajtak}";
                }));
        }
    }
}
