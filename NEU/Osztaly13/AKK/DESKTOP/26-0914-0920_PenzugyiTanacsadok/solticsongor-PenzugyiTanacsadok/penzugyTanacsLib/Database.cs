using System.Text;

namespace penzugyTanacsLib;

public class Database
{
    public List<Ugyfel> UgyfelList { get; set; } = new();
    public List<Szakterulet> SzakteruletList { get; set; } = new();
    public List<Tanacsado> TanacsadoList { get; set; } = new();
    public List<Talalkozo> TalalkozoList { get; set; } = new();

    private static string ResolvePath(string path)
    {
        if (File.Exists(path)) return path;

        string filename = Path.GetFileName(path);
        string[] candidates =
        {
            path,
            filename,
            Path.Combine("Tanacsado", filename),
            Path.Combine("TanacsadoGUI", filename),
            Path.Combine("..", filename),
            Path.Combine("../..", filename),
            Path.Combine("../../..", filename),
            Path.Combine("../../../..", filename),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename)
        };

        return candidates.FirstOrDefault(File.Exists) ?? path;
    }

    private static List<T> LoadList<T>(string path, Func<string, T> factory)
    {
        path = ResolvePath(path);

        if (!File.Exists(path)) return new List<T>();

        return File.ReadAllLines(path, Encoding.UTF8)
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(factory)
            .ToList();
    }

    public void LoadData(string ugyfelPath, string szakteruletPath, string tanacsadoPath, string talalkozoPath)
    {
        UgyfelList = LoadList(ugyfelPath, line => new Ugyfel(line));
        SzakteruletList = LoadList(szakteruletPath, line => new Szakterulet(line));
        TanacsadoList = LoadList(tanacsadoPath, line => new Tanacsado(line));
        TalalkozoList = LoadList(talalkozoPath, line => new Talalkozo(line));
    }
}