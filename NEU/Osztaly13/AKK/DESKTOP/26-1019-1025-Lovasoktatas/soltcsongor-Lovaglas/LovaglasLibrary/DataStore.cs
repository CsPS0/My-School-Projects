namespace LovaglasLibrary
{
    public class DataStore
    {
        readonly List<Szint> szint;
        readonly List<Kepzes> kepzes;
        readonly List<Oktato> oktato;
        readonly List<Orarend> orarend;

        public IEnumerable<Szint> Szintek => szint;
        public IEnumerable<Kepzes> Kepzes => kepzes;
        public IEnumerable<Oktato> Oktatok => oktato;
        public IEnumerable<Orarend> Orarend => orarend;

        DataStore(string path = "Forrasok")
        {
            szint = File.ReadAllLines(Path.Combine(path, "szint.csv"))
                .Skip(1)
                .Select(x => new Szint(x))
                .ToList();
            kepzes = File.ReadAllLines(Path.Combine(path, "kepzes.csv"))
                .Skip(1)
                .Select(x => new Kepzes(x))
                .ToList();
            oktato = File.ReadAllLines(Path.Combine(path, "oktato.csv"))
                .Skip(1)
                .Select(x => new Oktato(x))
                .ToList();
            orarend = File.ReadAllLines(Path.Combine(path, "orarend.csv"))
                .Skip(1)
                .Select(x => new Orarend(x))
                .ToList();
        }

        public static DataStore? Instance { get; set; }

        public static void InitCSV()
        {
            if (Instance is not null)
                throw new InvalidOperationException("Már incializált.");
            Instance = new DataStore();
        }
    }
}
