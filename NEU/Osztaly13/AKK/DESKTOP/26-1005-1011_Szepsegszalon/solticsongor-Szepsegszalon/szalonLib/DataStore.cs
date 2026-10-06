namespace szalonLib
{
    public class DataStore
    {
        readonly List<Alkalmazottak> alkalmazottak;
        readonly List<Kezelesek> kezelesek;
        readonly List<Szakmak> szakmak;
        readonly List<Vendegek> vendegek;

        DataStore()
        {
            alkalmazottak = File.ReadAllLines("\\..\\Console\\Forrasok\\alkalmazottak.csv")
                .Skip(1)
                .Select(x => new Alkalmazottak(x))
                .ToList();
            kezelesek = File.ReadAllLines("\\..\\Console\\Forrasok\\kezelesek.csv")
                .Skip(1)
                .Select(x => new Kezelesek(x))
                .ToList();
            szakmak = File.ReadAllLines("\\..\\Console\\Forrasok\\szakmak.csv")
                .Skip(1)
                .Select(x =>new Szakmak(x))
                .ToList();
            vendegek = File.ReadAllLines("\\..\\Console\\Forrasok\\vendegek.csv")
                .Skip(1)
                .Select(x => new Vendegek(x))
                .ToList();
        }

        public static DataStore? Instance { get; private set; }

        public static void InitCSV()
        {
            if (Instance is not null)
            {
                throw new InvalidOperationException("Már inicializált!");
            }
            Instance = new DataStore();
        }

        public IEnumerable<Alkalmazottak> Alkalmazottak_ => alkalmazottak;
        public IEnumerable<Kezelesek> Kezelesek_ => kezelesek;
        public IEnumerable<Szakmak> Szakmak_ => szakmak;
        public IEnumerable<Vendegek> Vendegek_ => vendegek;
    }
}
