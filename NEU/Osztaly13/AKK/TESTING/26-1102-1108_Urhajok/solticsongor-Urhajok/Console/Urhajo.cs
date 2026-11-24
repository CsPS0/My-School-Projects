namespace ConsoleApp;

public class Urhajo
{
    public string Nev { get; set; }
    public UrhajoTipus Tipus { get; set; }
    public UrhajoOsztaly Osztaly { get; set; }
    public int GyartasiEv { get; set; }
    public int Ferohely { get; set; }
    public List<LegenysegiTag> Legenyseg { get; set; }

    public Urhajo(string nev, UrhajoTipus tipus, UrhajoOsztaly osztaly, int gyartasiEv, int ferohely)
    {
        if (string.IsNullOrEmpty(nev))
        {
            throw new ArgumentException("Az űrhajó neve kötelező!");
        }

        if (ferohely <= 0)
        {
            throw new ArgumentException("A férőhelynek legalább 1-nek kell lennie!");
        }

        Nev = nev;
        Tipus = tipus;
        Osztaly = osztaly;
        GyartasiEv = gyartasiEv;
        Ferohely = ferohely;
        Legenyseg = new List<LegenysegiTag>();
    }

    public bool Indithato
    {
        get
        {
            foreach (var elvart in Tipus.ElvartSzakertelmek)
            {
                int osszeg = 0;
                foreach (var tag in Legenyseg)
                {
                    osszeg += tag.GetSzakertelemSzint(elvart.Key);
                }

                if (osszeg <= elvart.Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
