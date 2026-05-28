namespace szoftverLib;

public class Gep
{
    public int Id { get; set; }
    public string Location { get; set; }
    public string Type { get; set; }
    public string IpAddress { get; set; }

    public Gep(string line)
    {
        var parts = line.Split('\t');
        Id = int.Parse(parts[0]);
        Location = parts[1].Trim();
        Type = parts[2].Trim();
        IpAddress = parts[3].Trim();
    }
}
