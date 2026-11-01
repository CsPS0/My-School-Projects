namespace filmekLib;

public class DataStore
{
    public List<Alkotok> Alkotok { get; set; } = new();
    public List<Filmek> Filmek { get; set; } = new();
    public List<Filmstab> Filmstab { get; set; } = new();
    public List<Munkakor> Munkakor { get; set; } = new();

    public DataStore(string folder = "Forrasok")
    {
        Alkotok = File.ReadAllLines(Path.Combine(folder, "alkotok.txt")).Skip(1).Select(x => new Alkotok(x)).ToList();
        Filmek = File.ReadAllLines(Path.Combine(folder, "filmek.txt")).Skip(1).Select(x => new Filmek(x)).ToList();
        Filmstab = File.ReadAllLines(Path.Combine(folder, "filmstab.txt")).Skip(1).Select(x => new Filmstab(x)).ToList();
        Munkakor = File.ReadAllLines(Path.Combine(folder, "munkakor.txt")).Skip(1).Select(x => new Munkakor(x)).ToList();
    }

    // 4. feladat
    public int Feladat4()
    {
        return Alkotok.Count(a => a.Szuletett != null && a.Szuletett.Value.Year >= 1900 && a.Szuletett.Value.Year < 2000);
    }

    // 5. feladat
    public int Feladat5()
    {
        return Filmek.Count(f => f.HanyEveKeszult == 50);
    }

    // 6. feladat
    public List<Filmek> Feladat6()
    {
        return Filmek
            .Where(f => f.Hossz <= 45)
            .OrderBy(f => f.Hossz)
            .ToList();
    }

    // 7. feladat
    public (Filmek? Film, List<string> Rendezok) Feladat7(string title)
    {
        var film = Filmek.FirstOrDefault(f => f.Cim.ToLower() == title.ToLower());
        if (film == null)
        {
            return (null, new List<string>());
        }

        var directors = Filmstab
            .Where(s => s.FilmAzon == film.FilmAzon && s.MunkakorAzon == 1)
            .Select(s => Alkotok.First(a => a.AlkotoAzon == s.AlkotoAzon).Nev)
            .ToList();

        return (film, directors);
    }

    // 8. feladat
    public List<(string Nev, int Db)> Feladat8()
    {
        return Filmstab
            .GroupBy(s => s.AlkotoAzon)
            .OrderByDescending(g => g.Count())
            .Take(2)
            .Select(g => (Alkotok.First(a => a.AlkotoAzon == g.Key).Nev, g.Count()))
            .ToList();
    }

    // 9. feladat
    public List<(string FilmCim, List<string> Szineszek)> Feladat9()
    {
        var result = new List<(string FilmCim, List<string> Szineszek)>();
        var dajka = Alkotok.FirstOrDefault(a => a.Nev == "Dajka Margit");
        if (dajka == null)
        {
            return result;
        }

        var dajkaFilmIds = Filmstab
            .Where(s => s.AlkotoAzon == dajka.AlkotoAzon)
            .Select(s => s.FilmAzon)
            .Distinct();

        var dajkaFilms = Filmek
            .Where(f => dajkaFilmIds.Contains(f.FilmAzon))
            .OrderBy(f => f.Cim);

        foreach (var film in dajkaFilms)
        {
            var actors = Filmstab
                .Where(s => s.FilmAzon == film.FilmAzon && (s.MunkakorAzon == 6 || s.MunkakorAzon == 7) && s.AlkotoAzon != dajka.AlkotoAzon)
                .Select(s => Alkotok.First(a => a.AlkotoAzon == s.AlkotoAzon).Nev)
                .Distinct()
                .OrderBy(name => name)
                .ToList();

            result.Add((film.Cim, actors));
        }

        return result;
    }
}
