namespace filmekLib;

public class Filmstab
{
    public int FilmAzon { get; set; }
    public int MunkakorAzon { get; set; }
    public int AlkotoAzon { get; set; }

    public Filmstab(string row)
    {
        string[] parts = row.Split('\t');
        FilmAzon = int.Parse(parts[0]);
        MunkakorAzon = int.Parse(parts[1]);
        AlkotoAzon = int.Parse(parts[2]);
    }
}
