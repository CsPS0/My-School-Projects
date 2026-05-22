using System.Text;

namespace irodaLib;

public class AdatSzolgaltatas
{
    public List<Fordito> Forditok { get; private set; }
    public List<Nyelv> Nyelvek { get; private set; }
    public List<Megrendeles> Megrendelesek { get; private set; }

    public AdatSzolgaltatas(string forditoPath, string nyelvPath, string megrendelesPath)
    {
        Nyelvek = File.ReadAllLines(nyelvPath, Encoding.UTF8)
            .Skip(1)
            .Select(s => new Nyelv(s))
            .ToList();

        Forditok = File.ReadAllLines(forditoPath, Encoding.UTF8)
            .Skip(1)
            .Select(s => new Fordito(s))
            .ToList();

        Megrendelesek = File.ReadAllLines(megrendelesPath, Encoding.UTF8)
            .Skip(1)
            .Select(s => new Megrendeles(s))
            .ToList();
    }
}
