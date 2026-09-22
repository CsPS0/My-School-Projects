namespace kolcsonzoLib;

public static class SporteszkozFactory
{
    public static Sporteszkoz Factory(string datas)
    {
        string[] data = datas.Split(';');
        string tipus = data[0].Trim();
        string azonosito = data[1].Trim();
        string leiras = data[2].Trim();
        int meret = int.Parse(data[3].Trim());

        return tipus.ToUpper() switch
        {
            "L" => new Silec(azonosito, leiras, meret),
            "B" => new Snowboard(azonosito, leiras, meret, data.Length > 4 && data[4].Trim().ToLower() == "c"),
            _ => throw new NotSupportedException($"Ismeretlen típus: {tipus}")
        };
    }
}