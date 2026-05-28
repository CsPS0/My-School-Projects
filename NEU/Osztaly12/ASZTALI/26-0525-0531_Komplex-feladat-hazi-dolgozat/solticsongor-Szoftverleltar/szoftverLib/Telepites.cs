namespace szoftverLib;

public class Telepites
{
    public int MachineId { get; set; }
    public int SoftwareId { get; set; }
    public string Version { get; set; }
    public DateTime? Date { get; set; }

    public Telepites(string line)
    {
        var parts = line.Split('\t');
        MachineId = int.Parse(parts[0]);
        SoftwareId = int.Parse(parts[1]);
        Version = parts[2].Trim();
        
        if (!string.IsNullOrWhiteSpace(parts[3]) && DateTime.TryParse(parts[3].Trim(), out DateTime date))
        {
            Date = date;
        }
        else
        {
            Date = null;
        }
    }
}
