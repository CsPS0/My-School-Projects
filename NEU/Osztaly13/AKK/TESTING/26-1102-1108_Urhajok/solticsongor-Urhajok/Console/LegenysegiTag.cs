namespace ConsoleApp;

public class LegenysegiTag
{
    public string Nev { get; set; }
    public string Nemzetiseg { get; set; }
    public DateTime SzuletesiDatum { get; set; }
    public string Nem { get; set; }
    public Dictionary<string, int> Szakertelmek { get; set; }
    public Urhajo? HozzaRendeltHajo { get; set; }

    public LegenysegiTag(string nev, string nemzetiseg, DateTime szuletesiDatum, string nem, Dictionary<string, int>? szakertelmek = null)
    {
        if (string.IsNullOrEmpty(nev))
        {
            throw new ArgumentException("A legénységi tag neve kötelező!");
        }

        Nev = nev;
        Nemzetiseg = nemzetiseg;
        SzuletesiDatum = szuletesiDatum;
        Nem = nem;
        Szakertelmek = szakertelmek ?? new Dictionary<string, int>();
    }

    public int GetSzakertelemSzint(string szakertelemNev)
    {
        foreach (var item in Szakertelmek)
        {
            if (item.Key.ToLower() == szakertelemNev.ToLower())
            {
                return item.Value;
            }
        }
        return 0;
    }
}
