namespace filmekLib;

public class Munkakor
{
    public int MunkakorAzon { get; set; }
    public string Nev { get; set; }

    public Munkakor(string row)
    {
        string[] parts = row.Split('\t');
        MunkakorAzon = int.Parse(parts[0]);
        Nev = parts[1];
    }
}
