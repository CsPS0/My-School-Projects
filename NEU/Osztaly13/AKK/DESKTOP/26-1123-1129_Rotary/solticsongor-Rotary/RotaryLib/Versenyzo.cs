namespace RotaryLib;

public record Versenyzo
{
    public int Poz { get; set; }
    public string Rajtsz { get; set; } = string.Empty;
    public int Szul { get; set; }
    public string Telepules { get; set; } = string.Empty;
    public string Orszagkod { get; set; } = string.Empty;
    public int Kategoria { get; set; }
    public int Kpoz { get; set; }
    public string Nem { get; set; } = string.Empty;
    public int Npoz { get; set; }
    public string Ido { get; set; } = string.Empty;

    public static Versenyzo? CreateCSV(string line)
    {
        string[] data = line.Split(';');
        if (data.Length != 10)
        {
            return null;
        }
        if (!int.TryParse(data[0], out int poz))
        {
            return null;
        }
        return new Versenyzo()
        {
            Poz = poz,
            Rajtsz = data[1],
            Szul = int.Parse(data[2]),
            Telepules = data[3],
            Orszagkod = data[4],
            Kategoria = int.Parse(data[5]),
            Kpoz = int.Parse(data[6]),
            Nem = data[7],
            Npoz = int.Parse(data[8]),
            Ido = data[9]
        };
    }

    public static bool JoIdo(string ido)
    {
        string[] reszek = ido.Split(':');
        if (reszek.Length != 3)
        {
            return false;
        }
        if (!int.TryParse(reszek[0], out int ora) || !int.TryParse(reszek[1], out int perc) || !int.TryParse(reszek[2], out int mp))
        {
            return false;
        }
        if (reszek[1].Length != 2 || reszek[2].Length != 2)
        {
            return false;
        }
        return ora >= 0 && perc >= 0 && perc < 60 && mp >= 0 && mp < 60;
    }
}
