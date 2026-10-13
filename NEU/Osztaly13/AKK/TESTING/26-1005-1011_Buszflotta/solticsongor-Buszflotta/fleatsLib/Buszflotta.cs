namespace fleatsLib;

public class Buszflotta
{
    readonly List<Busz> buses = new();
    readonly Dictionary<string, int> stopOn = new(StringComparer.OrdinalIgnoreCase);
    readonly Dictionary<string, int> stopOff = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyList<Busz> Buszok => buses.AsReadOnly();
    public int BuszokSzama => buses.Count;

    public Buszflotta()
    {
    }

    public Buszflotta(IEnumerable<Busz> initBuses)
    {
        if (initBuses == null)
        {
            throw new ArgumentNullException(nameof(initBuses));
        }

        foreach (var b in initBuses)
        {
            BuszHozzaadas(b);
        }
    }

    public void BuszHozzaadas(Busz b)
    {
        if (b == null)
        {
            throw new ArgumentNullException(nameof(b), "A hozzáadandó busz nem lehet null.");
        }

        if (buses.Any(x => x.Rendszam.Equals(b.Rendszam, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException($"Már létezik '{b.Rendszam}' rendszámú busz a flottában.", nameof(b));
        }

        buses.Add(b);
    }

    public Busz? KeresBusz(string plate)
    {
        if (string.IsNullOrWhiteSpace(plate))
        {
            return null;
        }

        return buses.FirstOrDefault(x => x.Rendszam.Equals(plate.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public int MegalloEsemeny(string plate, string stopName, int off, int on)
    {
        if (string.IsNullOrWhiteSpace(stopName))
        {
            throw new ArgumentException("A megálló neve nem lehet üres vagy null.", nameof(stopName));
        }

        Busz b = KeresBusz(plate)
            ?? throw new KeyNotFoundException($"Nem található '{plate}' rendszámú busz a flottában.");

        string key = stopName.Trim();
        int left = b.MegallobanKezeles(off, on);
        int boarded = on - left;

        stopOff[key] = stopOff.GetValueOrDefault(key, 0) + off;
        stopOn[key] = stopOn.GetValueOrDefault(key, 0) + boarded;

        return left;
    }

    public int GetFelszalltUtasok(string stopName)
    {
        if (string.IsNullOrWhiteSpace(stopName))
        {
            return 0;
        }

        return stopOn.GetValueOrDefault(stopName.Trim(), 0);
    }

    public int GetLeszalltUtasok(string stopName)
    {
        if (string.IsNullOrWhiteSpace(stopName))
        {
            return 0;
        }

        return stopOff.GetValueOrDefault(stopName.Trim(), 0);
    }

    public int GetBuszOsszesSzallitottUtas(string plate)
    {
        Busz b = KeresBusz(plate)
            ?? throw new KeyNotFoundException($"Nem található '{plate}' rendszámú busz a flottában.");

        return b.OsszesSzallitottUtas;
    }

    public int GetFlottaOsszesSzallitottUtas()
    {
        return buses.Sum(x => x.OsszesSzallitottUtas);
    }

    public IReadOnlyDictionary<string, int> GetOsszesFelszallasMegallonkent()
    {
        return stopOn.AsReadOnly();
    }

    public IReadOnlyDictionary<string, int> GetOsszesLeszallasMegallonkent()
    {
        return stopOff.AsReadOnly();
    }

    public void NapVegeReset()
    {
        foreach (var b in buses)
        {
            b.ResetNapiAdatok();
        }

        stopOn.Clear();
        stopOff.Clear();
    }
}
