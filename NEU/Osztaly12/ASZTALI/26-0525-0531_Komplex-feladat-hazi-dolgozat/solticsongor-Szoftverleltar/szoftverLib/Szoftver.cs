namespace szoftverLib;

public class Szoftver
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    public string KategoriaSzoftverNev => $"{Category} - {Name}";

    public Szoftver(string line)
    {
        var parts = line.Split('\t');
        Id = int.Parse(parts[0]);
        Name = parts[1].Trim();
        Category = parts[2].Trim();
    }
}
