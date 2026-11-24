namespace ConsoleApp;

public class UrhajoTipus
{
    public string Nev { get; set; }
    public string Leiras { get; set; }
    public Dictionary<string, int> ElvartSzakertelmek { get; set; }

    public UrhajoTipus(string nev, string leiras, Dictionary<string, int>? elvartSzakertelmek = null)
    {
        if (string.IsNullOrEmpty(nev))
        {
            throw new ArgumentException("A típus neve kötelező!");
        }

        Nev = nev;
        Leiras = leiras;
        ElvartSzakertelmek = elvartSzakertelmek ?? new Dictionary<string, int>();
    }
}
