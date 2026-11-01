namespace filmekLib;

public class Filmek
{
    public int FilmAzon { get; set; }
    public string Cim { get; set; }
    public int Ev { get; set; }
    public string Szines { get; set; }
    public string Mufaj { get; set; }
    public int Hossz { get; set; }

    public int HanyEveKeszult => DateTime.Now.Year - Ev;

    public Filmek(string row)
    {
        string[] parts = row.Split('\t');
        FilmAzon = int.Parse(parts[0]);
        Cim = parts[1];
        Ev = int.Parse(parts[2]);
        Szines = parts[3];
        Mufaj = parts[4];
        Hossz = int.Parse(parts[5]);
    }
}
