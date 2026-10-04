namespace cukraszdaLib
{
    public class Sutemenyek
    {
        private readonly Dictionary<string, Sutemeny> sutemenyTarolo;

        public Sutemenyek(IEnumerable<Sutemeny> sutemenyek)
        {
            sutemenyTarolo = sutemenyek.ToDictionary(s => s.Azonosito, s => s);
        }

        public Sutemeny this[string azonosito] => sutemenyTarolo[azonosito];

        public bool Tartalmaz(string azonosito) => sutemenyTarolo.ContainsKey(azonosito);

        public IEnumerable<Sutemeny> SutemenyTipusok =>
            sutemenyTarolo.Values
                .Where(s => !s.Tipus.StartsWith("d", StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.Azonosito, StringComparer.Ordinal);
    }
}
