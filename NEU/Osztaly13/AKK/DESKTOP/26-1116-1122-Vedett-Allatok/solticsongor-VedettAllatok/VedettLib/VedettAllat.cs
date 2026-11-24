namespace VedettLib;

public record VedettAllat
{
    public int ID { get; set; }
    public string Nev { get; set; } = string.Empty;
    public string Ertek { get; set; } = string.Empty;
    public int Ev { get; set; }
    public string Kategoria { get; set; } = string.Empty;

    public static VedettAllat? CreateCSV(string line)
    {
        string[] data = line.Split(';');
        if (data.Length != 5)
        {
            return null;
        }
        if (!int.TryParse(data[0], out int id))
        {
            return null;
        }
        if (!int.TryParse(data[3], out int ev))
        {
            return null;
        }
        return new VedettAllat()
        {
            ID = id,
            Nev = data[1],
            Ertek = data[2],
            Ev = ev,
            Kategoria = data[4]
        };
    }
}
