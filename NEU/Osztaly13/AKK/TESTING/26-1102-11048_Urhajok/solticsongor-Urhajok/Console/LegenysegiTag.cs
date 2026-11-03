namespace ConsoleApp;

public class LegenysegiTag
{
    public string Nev { get; }
    public string Nemzetiseg { get; }
    public DateTime SzuletesiDatum { get; }
    public string Nem { get; }
    public Dictionary<string, int> Szakertelmek { get; }
    public Urhajo? HozzaRendeltHajo { get; internal set; }

    public LegenysegiTag(string nev, string nemzetiseg, DateTime szuletesiDatum, string nem, Dictionary<string, int>? szakertelmek = null)
    {
        if (string.IsNullOrWhiteSpace(nev))
        {
            throw new ArgumentException("A legénységi tag neve kötelező.", nameof(nev));
        }

        Nev = nev;
        Nemzetiseg = nemzetiseg ?? string.Empty;
        SzuletesiDatum = szuletesiDatum;
        Nem = nem ?? string.Empty;
        Szakertelmek = szakertelmek != null
            ? new Dictionary<string, int>(szakertelmek, StringComparer.OrdinalIgnoreCase)
            : new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    }

    public int GetSzakertelemSzint(string szakertelemNev)
    {
        return Szakertelmek.TryGetValue(szakertelemNev, out int szint) ? szint : 0;
    }
}
