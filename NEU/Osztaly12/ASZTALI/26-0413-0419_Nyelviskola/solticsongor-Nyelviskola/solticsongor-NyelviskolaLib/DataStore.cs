using System.Text.Json;

namespace solticsongor_NyelviskolaLib;

public class DataStore
{
    private readonly List<Tanar> tanarok;
    private readonly List<TanitasiAlkalom> tanitasiAlkalmak;
    private readonly List<Nyelv> nyelvek;

    private DataStore()
    {
        tanarok = File.ReadAllLines("Input\\tanar.csv")
            .Skip(1)
            .Select(x => new Tanar(x))
            .ToList();
        tanitasiAlkalmak = File.ReadAllLines("Input\\tanitasi_alkalom.csv")
            .Skip(1)
            .Select(x => new TanitasiAlkalom(x))
            .ToList();
        nyelvek = File.ReadAllLines("Input\\nyelv.csv")
            .Skip(1)
            .Select(x => new Nyelv(x))
            .ToList();
    }

    public static DataStore? Instance { get; private set; }

    public static void InitCSV()
    {
        if (Instance is not null) return;
        Instance = new DataStore();
    }

    public void ExportToJson()
    {
        File.WriteAllText("tanar.json", JsonSerializer.Serialize(tanarok));
        File.WriteAllText("tanitasi_alkalom.json", JsonSerializer.Serialize(tanitasiAlkalmak));
        File.WriteAllText("nyelv.json", JsonSerializer.Serialize(nyelvek));
    }

    public IEnumerable<Tanar> Tanarok => tanarok;
    public IEnumerable<TanitasiAlkalom> TanitasiAlkalmak => tanitasiAlkalmak;
    public IEnumerable<Nyelv> Nyelvek => nyelvek;
}
