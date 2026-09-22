namespace kolcsonzoLib;

public class Kolcsonzesek
{
    private readonly List<Sporteszkoz> eszkozok;

    public Kolcsonzesek(IEnumerable<Sporteszkoz> sporteszkozok)
    {
        eszkozok = new List<Sporteszkoz>(sporteszkozok);
    }

    public List<string> KolcsonozhetoEszkozokLeirasai
    {
        get
        {
            List<string> leirasok = new List<string>();
            foreach (Sporteszkoz e in eszkozok)
            {
                leirasok.Add(e.Leiras);
            }
            return leirasok;
        }
    }

    public Sporteszkoz this[string azonosito]
    {
        get
        {
            foreach (Sporteszkoz e in eszkozok)
            {
                if (e.Azonosito.Equals(azonosito, StringComparison.OrdinalIgnoreCase))
                {
                    return e;
                }
            }
            throw new KeyNotFoundException($"Nincs ilyen eszköz: {azonosito}");
        }
    }

    public List<string> SporteszkozBerles(IEnumerable<Berles> igenyek)
    {
        List<string> hibak = new List<string>();

        foreach (Berles b in igenyek)
        {
            Sporteszkoz? talalat = null;
            foreach (Sporteszkoz e in eszkozok)
            {
                if (e.Azonosito.Equals(b.SporteszkozID, StringComparison.OrdinalIgnoreCase))
                {
                    talalat = e;
                    break;
                }
            }

            if (talalat == null)
            {
                continue;
            }

            try
            {
                talalat.UjBerles(b);
            }
            catch (HibasFoglalasException ex)
            {
                hibak.Add($"{ex.Message}\t{b.Berleskezdet:yyyy. MM. dd.} - {b.BerlesVege:yyyy. MM. dd.} {b.BerloNeve}, {b.SporteszkozID}");
            }
        }

        return hibak;
    }

    public List<Sporteszkoz> SzabadSporteszkozok(DateOnly kezdet, int napokSzama)
    {
        List<Sporteszkoz> szabadok = new List<Sporteszkoz>();
        foreach (Sporteszkoz e in eszkozok)
        {
            if (e.Foglalasok.SzabadE(kezdet, napokSzama))
            {
                szabadok.Add(e);
            }
        }
        return szabadok;
    }

    public List<Sporteszkoz> Eszkozok => eszkozok;
}