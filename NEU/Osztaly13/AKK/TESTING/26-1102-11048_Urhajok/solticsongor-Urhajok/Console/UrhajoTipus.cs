namespace ConsoleApp;

public class UrhajoTipus
{
    public string Nev { get; }
    public string Leiras { get; }
    public Dictionary<string, int> ElvartSzakertelmek { get; }

    public UrhajoTipus(string nev, string leiras, Dictionary<string, int>? elvartSzakertelmek = null)
    {
        if (string.IsNullOrWhiteSpace(nev))
        {
            throw new ArgumentException("Az űrhajó típus neve kötelező.", nameof(nev));
        }

        Nev = nev;
        Leiras = leiras ?? string.Empty;
        ElvartSzakertelmek = elvartSzakertelmek != null
            ? new Dictionary<string, int>(elvartSzakertelmek, StringComparer.OrdinalIgnoreCase)
            : new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    }
}
