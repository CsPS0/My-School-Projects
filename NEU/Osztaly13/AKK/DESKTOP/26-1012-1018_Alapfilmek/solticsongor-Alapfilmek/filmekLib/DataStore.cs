namespace filmekLib
{
    public class DataStore
    {
        public List<Alkotok> Alkotok { get; set; }
        public List<Filmek> Filmek { get; set; }
        public List<Filmstab> Filmstab { get; set; }
        public List<Munkakor> Munkakor { get; set; }

        public DataStore(string files = "..\\Console\\Forrasok")
        {
            Alkotok = File.ReadAllLines($"{files}/alkotok.txt")
                .Skip( 1 )
                .Select(x => new Alkotok(x))
                .ToList();
            Filmek = File.ReadAllLines($"{files}/filmek.txt")
                .Skip(1)
                .Select(x => new Filmek(x))
                .ToList();
            Filmstab = File.ReadAllLines($"{files}/filmstab.txt")
                .Skip(1)
                .Select(x => new Filmstab(x))
                .ToList();
            Munkakor = File.ReadAllLines($"{files}/munkakor.txt")
                .Skip(1)
                .Select(x => new Munkakor(x))
                .ToList();
        }

        public void Feladatok(string output)
        {
            var szuletett20 = Alkotok.FirstOrDefault(x => x.Szuletett == 1900.01.01);
        }
    }
}
