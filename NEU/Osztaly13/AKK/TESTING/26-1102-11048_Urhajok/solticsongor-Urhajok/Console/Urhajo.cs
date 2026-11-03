namespace ConsoleApp;

public class Urhajo
{
    public string Nev { get; }
    public UrhajoTipus Tipus { get; }
    public UrhajoOsztaly Osztaly { get; }
    public int GyartasiEv { get; }
    public int Ferohely { get; }
    public List<LegenysegiTag> Legenyseg { get; } = new();

    public bool Indithato
    {
        get
        {
            if (Tipus.ElvartSzakertelmek.Count == 0)
            {
                return true;
            }

            foreach (var elvart in Tipus.ElvartSzakertelmek)
            {
                int osszeg = Legenyseg.Sum(tag => tag.GetSzakertelemSzint(elvart.Key));
                if (osszeg <= elvart.Value)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public Urhajo(string nev, UrhajoTipus tipus, UrhajoOsztaly osztaly, int gyartasiEv, int ferohely)
    {
        if (string.IsNullOrWhiteSpace(nev))
        {
            throw new ArgumentException("Az űrhajó neve kötelező.", nameof(nev));
        }

        if (ferohely <= 0)
        {
            throw new ArgumentException("A férőhelynek legalább 1-nek kell lennie.", nameof(ferohely));
        }

        Nev = nev;
        Tipus = tipus ?? throw new ArgumentNullException(nameof(tipus));
        Osztaly = osztaly ?? throw new ArgumentNullException(nameof(osztaly));
        GyartasiEv = gyartasiEv;
        Ferohely = ferohely;
    }
}
