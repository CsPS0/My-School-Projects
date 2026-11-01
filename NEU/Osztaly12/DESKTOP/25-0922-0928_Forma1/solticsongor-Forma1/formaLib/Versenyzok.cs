namespace formaLib;

public class Versenyzok
{
    public List<Versenyzo> Adatok { get; }

    public Versenyzok(IEnumerable<string> sorok)
    {
        var validVersenyzok = new List<Versenyzo>();
        foreach (var sor in sorok)
        {
            try
            {
                validVersenyzok.Add(new Versenyzo(sor));
            }
            catch (FormatException)
            {
                //asd
            }
        }
        Adatok = validVersenyzok;
    }

    // 2. Feladat
    public IEnumerable<Versenyzo> GetHillVersenyzok()
    {
        return Adatok
            .Where(v => v.Nev.EndsWith(" Hill") && v.Szuldat.HasValue)
            .GroupBy(v => v.Nev)
            .Select(g => g.First())
            .OrderBy(v => v.Szuldat);
    }

    // 3. Feladat
    public string GetFutamgyoztesekAsString()
    {
        var futamgyoztesek = Adatok
            .Where(v => v.Helyezes == 1)
            .Select(v => v.Nev)
            .Distinct()
            .ToList();
        return $"{string.Join(", \n\t- ", futamgyoztesek)}";
    }

    // 4. Feladat
    public int? GetFangioElsoVersenyKora()
    {
        var fangioElsoVerseny = Adatok
            .Where(v => v.Nev == "Juan-Manuel Fangio")
            .OrderBy(v => v.Datum)
            .FirstOrDefault();

        if (fangioElsoVerseny != null && fangioElsoVerseny.Szuldat.HasValue)
        {
            var elsoVersenyDatum = fangioElsoVerseny.Datum;
            var szuletesDatum = fangioElsoVerseny.Szuldat.Value;
            var kor = elsoVersenyDatum.Year - szuletesDatum.Year;
            if (elsoVersenyDatum.DayOfYear < szuletesDatum.DayOfYear)
            {
                kor--;
            }
            return kor;
        }
        return null;
    }

    // 5. Feladat
    public IEnumerable<KeyValuePair<string, int>> GetFerrariLeggyakoribbHibak()
    {
        return Adatok
            .Where(v => (v.Csapat != null && v.Csapat.Contains("Ferrari")) && !string.IsNullOrEmpty(v.Hiba))
            .GroupBy(v => v.Hiba)
            .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()))
            .OrderByDescending(x => x.Value)
            .Take(3);
    }

    // 6. Feladat
    public int GetCsapatNelkuliekSzama()
    {
        return Adatok
            .Where(v => string.IsNullOrEmpty(v.Csapat))
            .Select(v => v.Nev)
            .Distinct()
            .Count();
    }

    // 7. Feladat
    public string GetKesobbiHelyszinekAsString()
    {
        var elsoMagyarNagydij = Adatok.FirstOrDefault(v => v.Helyszin == "Magyarország");
        if (elsoMagyarNagydij != null)
        {
            var kesobbiHelyszinek = Adatok
                .Where(v => v.Datum > elsoMagyarNagydij.Datum)
                .Select(v => v.Helyszin)
                .Distinct()
                .ToList();
            if (kesobbiHelyszinek.Any())
            {
                return $"{string.Join(", ", kesobbiHelyszinek)}";
            }
        }
        return "Nincs adat magyar nagydíjról vagy az azt követő versenyekről.";
    }

    // 8. Feladat
    public IOrderedEnumerable<IGrouping<int, Versenyzo>> GetMonacoEredmenyek()
    {
        return Adatok
            .Where(v => v.Helyszin == "Monaco" && v.Helyezes.HasValue && v.Helyezes <= 6)
            .GroupBy(v => v.Datum.Year)
            .OrderBy(g => g.Key);
    }
}