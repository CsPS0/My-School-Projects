namespace KutyakLib
{
    public class Kutyak
    {
        // 1.feladat
        readonly List<Kutya> kutyak = new();

        public Kutyak(IEnumerable<string> sor)
        {
            foreach (var i in sor)
            {
                kutyak.Add(new Kutya(i));
            }
        }

        public int Ossz => kutyak.Count;
        // 2.feladat
        public string Elveszettek => string.Join("\n\t", kutyak.Where(k => k.Mikor == new DateTime(2020, 12, 31)).Select(k => $"{k.Nev};{(k.Nem == "" ? "n.a." : k.Nem)};{k.Faj};{k.Hely}"));
        // 3.feladat
        public int Ev2020 => kutyak.Count(k => k.Mikor.Year == 2020);
        public int Ev2021 => kutyak.Count(k => k.Mikor.Year == 2021);
        // 4.feladat
        public string FajtakNevsor => string.Join("; ", kutyak.Select(k => k.Faj).Distinct().OrderBy(f => f));
        // 5.feladat
        public string TobbszorosNevek => string.Join("\n\t", kutyak.GroupBy(k => k.Nev).Where(g => g.Count() > 1).Select(g => $"{g.Key}: {g.Count()}"));
        // 6.feladat
        public int SzukakSzama => kutyak.Count(k => k.Nem == "szuka");
        public int KanokSzama => kutyak.Count(k => k.Nem == "kan");
        public int IsmeretlenNem => kutyak.Count(k => k.Nem == "");
        // 7.feladat
        public string KeruletiStatisztika => string.Join("\n\t", kutyak.Where(k => k.Hely.ToLower().Contains("kerület")).GroupBy(k => k.Hely).OrderBy(g => g.Key).Select(g => $"{g.Key}: {g.Count()}"));
        // 8.feladat
        public string FajtakLegalabb5Kutya => string.Join("\n\t", kutyak.GroupBy(k => k.Faj).Where(g => g.Count() >= 5).Select(g => g.Key));
        // 9.feladat
        public string HaviEltunesek => string.Join("\n", kutyak.GroupBy(k => k.Mikor.ToString("yyyy-MM")).OrderBy(g => g.Key).Select(g => $"{g.Key}: {g.Count()}"));
        // 10.feladat
        public string HelyNemStatisztika => string.Join("\n\t", kutyak.GroupBy(k => k.Hely).OrderBy(g => g.Key).Select(g =>
        {
            var szuka = g.Count(x => x.Nem == "szuka");
            var kan = g.Count(x => x.Nem == "kan");
            var na = g.Count(x => x.Nem == "");
            return $"{g.Key}: kan: {kan}, szuka: {szuka}, n.a.: {na}";
        }));
        // 11.feladat
        public string FajtaHelyek => string.Join("\n\t", kutyak.GroupBy(k => k.Faj).Select(g =>
        {
            var helyek = string.Join(", ", g.Select(k => k.Hely).Distinct().OrderBy(h => h));
            return $"{g.Key}: {helyek}";
        }));
        // 12.feladat
        public string NevFajtak => string.Join("\n\t", kutyak.GroupBy(k => k.Nev).Select(g =>
        {
            var fajtak = string.Join(", ", g.Select(k => k.Faj).Distinct().OrderBy(f => f));
            return $"{g.Key}: {fajtak}";
        }));
    }
}