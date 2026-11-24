namespace RotaryLib;

public class VersenyzoStore : IVersenyzoStore
{
    Dictionary<string, Versenyzo> _adat;

    public VersenyzoStore()
    {
        _adat = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "Eredmenyek.csv"))
            .Skip(1)
            .Select(Versenyzo.CreateCSV)
            .Where(x => x != null)
            .ToDictionary(x => x!.Rajtsz, x => x!);
    }

    public IEnumerable<Versenyzo> GetAll()
    {
        return _adat.Values.OrderBy(x => x.Poz);
    }

    public Versenyzo? GetById(string rajtsz)
    {
        if (_adat.TryGetValue(rajtsz, out Versenyzo? versenyzo))
        {
            return versenyzo;
        }
        return null;
    }

    public bool AddNew(Versenyzo versenyzo)
    {
        if (_adat.ContainsKey(versenyzo.Rajtsz))
        {
            return false;
        }
        if (_adat.Values.Any(x => x.Poz == versenyzo.Poz))
        {
            return false;
        }
        if (!Versenyzo.JoIdo(versenyzo.Ido))
        {
            return false;
        }
        _adat[versenyzo.Rajtsz] = versenyzo;
        return true;
    }
}
