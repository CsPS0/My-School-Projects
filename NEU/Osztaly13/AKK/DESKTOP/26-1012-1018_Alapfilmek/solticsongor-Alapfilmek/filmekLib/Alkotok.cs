namespace filmekLib;

public class Alkotok
{
    public int AlkotoAzon { get; set; }
    public string Nev { get; set; }
    public DateOnly? Szuletett { get; set; }
    public DateOnly? Elhunyt { get; set; }

    public Alkotok(string row)
    {
        string[] parts = row.Split('\t');
        AlkotoAzon = int.Parse(parts[0]);
        Nev = parts[1];
        if (parts.Length > 2 && parts[2] != "")
        {
            Szuletett = DateOnly.Parse(parts[2]);
        }
        if (parts.Length > 3 && parts[3] != "")
        {
            Elhunyt = DateOnly.Parse(parts[3]);
        }
    }
}
