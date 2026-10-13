namespace szalonLib;

public class DataStore
{
    public List<Alkalmazottak> Alkalmazottak { get; set; }
    public List<Kezelesek> Kezelesek { get; set; }
    public List<Szakmak> Szakmak { get; set; }
    public List<Vendegek> Vendegek { get; set; }

    public DataStore(string folder = "Forrasok")
    {
        Alkalmazottak = File.ReadAllLines($"{folder}/alkalmazottak.csv")
            .Skip(1)
            .Select(x => new Alkalmazottak(x))
            .ToList();

        Kezelesek = File.ReadAllLines($"{folder}/kezelesek.csv")
            .Skip(1)
            .Select(x => new Kezelesek(x))
            .ToList();

        Szakmak = File.ReadAllLines($"{folder}/szakmak.csv")
            .Skip(1)
            .Select(x => new Szakmak(x))
            .ToList();

        Vendegek = File.ReadAllLines($"{folder}/vendegek.csv")
            .Skip(1)
            .Select(x => new Vendegek(x))
            .ToList();
    }

    public int AlkalmazottBevetel(string nev)
    {
        var emp = Alkalmazottak.FirstOrDefault(x => x.Nev == nev);
        if (emp == null) return 0;
        return Kezelesek.Where(x => x.AlkalmazottID == emp.AlkalmazottID).Sum(x => x.Ar);
    }
}
