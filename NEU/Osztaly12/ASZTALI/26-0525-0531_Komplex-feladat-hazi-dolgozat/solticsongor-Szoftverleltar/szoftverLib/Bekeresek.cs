namespace szoftverLib;

public class Bekeresek
{
    public IEnumerable<Gep> Machines { get; private set; }
    public IEnumerable<Szoftver> Softwares { get; private set; }
    public IEnumerable<Telepites> Installations { get; private set; }

    public Bekeresek()
    {
        string actualBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        string gepekPath = Path.Combine(actualBasePath, "gep.txt");
        if (!File.Exists(gepekPath))
        {
            throw new FileNotFoundException($"A '{gepekPath}' fájl nem található!");
        }

        string szoftverPath = Path.Combine(actualBasePath, "szoftver.txt");
        if (!File.Exists(szoftverPath))
        {
            throw new FileNotFoundException($"A '{szoftverPath}' fájl nem található!");
        }

        string telepitesPath = Path.Combine(actualBasePath, "telepites.txt");
        if (!File.Exists(telepitesPath))
        {
            throw new FileNotFoundException($"A '{telepitesPath}' fájl nem található!");
        }

        Machines = File.ReadLines(gepekPath).Skip(1).Select(l => new Gep(l)).ToList();
        Softwares = File.ReadLines(szoftverPath).Skip(1).Select(l => new Szoftver(l)).ToList();
        Installations = File.ReadLines(telepitesPath).Skip(1).Select(l => new Telepites(l)).ToList();
    }

    public int GetOsszesGepSzama()
    {
        return Machines.Count();
    }

    public IEnumerable<IGrouping<string, Gep>> GetGepekTipusSzerint()
    {
        return Machines.GroupBy(g => g.Type);
    }

    public int GetTelepitettSzoftverekSzama(int machineId)
    {
        return Installations.Count(t => t.MachineId == machineId);
    }

    public Gep? GetGepById(int machineId)
    {
        return Machines.FirstOrDefault(g => g.Id == machineId);
    }

    public IEnumerable<Gep> GetGepekByKategoria(string category)
    {
        var softwareIds = Softwares.Where(s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).Select(s => s.Id);
        var machineIds = Installations.Where(t => softwareIds.Contains(t.SoftwareId)).Select(t => t.MachineId).Distinct();
        return Machines.Where(g => machineIds.Contains(g.Id));
    }

    public IEnumerable<string> GetKategoriak()
    {
        return Softwares.Select(s => s.Category).Distinct().OrderBy(c => c);
    }

    public IEnumerable<string> GetSzoftverNevekByKategoria(string category)
    {
        return Softwares.Where(s => s.Category == category).Select(s => s.Name).Distinct().OrderBy(n => n);
    }

    public IEnumerable<AdatElem> GetTelepitesekBySzoftverEsKategoria(string softwareName, string category)
    {
        var softwareIds = Softwares
            .Where(s => s.Name == softwareName && s.Category == category)
            .Select(s => s.Id);

        var matchingInstallations = Installations
            .Where(t => softwareIds.Contains(t.SoftwareId));

        return matchingInstallations.Select(t => 
        {
            var machine = Machines.FirstOrDefault(g => g.Id == t.MachineId);
            return new AdatElem
            {
                IpAddress = machine?.IpAddress ?? "Ismeretlen",
                Location = machine?.Location ?? "Ismeretlen",
                Version = string.IsNullOrEmpty(t.Version) ? "Ismeretlen" : t.Version
            };
        });
    }
}
