namespace VedettLib;

public class VedettAllatStore : IVedettAllatStore
{
    Dictionary<int, VedettAllat> _adat;

    public VedettAllatStore()
    {
        _adat = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "vedett.csv"))
            .Skip(1)
            .Select(VedettAllat.CreateCSV)
            .Where(x => x != null)
            .ToDictionary(x => x!.ID, x => x!);
    }

    public IEnumerable<VedettAllat> GetAll()
    {
        return _adat.Values;
    }

    public VedettAllat? GetById(int id)
    {
        if (_adat.TryGetValue(id, out VedettAllat? allat))
        {
            return allat;
        }
        return null;
    }

    public IEnumerable<VedettAllat> GetByYear(int year)
    {
        return _adat.Values.Where(x => x.Ev == year);
    }

    public IEnumerable<VedettAllat> GetByCategory(string category)
    {
        return _adat.Values.Where(x => x.Kategoria == category);
    }

    public IEnumerable<string> GetCategories()
    {
        return _adat.Values.Select(x => x.Kategoria).Distinct().OrderBy(x => x);
    }

    public int AddNew(VedettAllat allat)
    {
        int newId = 1;
        if (_adat.Count > 0)
        {
            newId = _adat.Keys.Max() + 1;
        }
        VedettAllat newAllat = allat with { ID = newId };
        _adat[newId] = newAllat;
        return newId;
    }

    public bool Update(int id, VedettAllat newAllat)
    {
        if (!_adat.ContainsKey(id))
        {
            return false;
        }
        _adat[id] = newAllat with { ID = id };
        return true;
    }

    public bool Delete(int id)
    {
        return _adat.Remove(id);
    }
}
