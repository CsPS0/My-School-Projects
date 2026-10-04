namespace cukraszdaLib
{
    public class KeszitesAdatok
    {
        private readonly Dictionary<string, KeszitesAdat> adatok;

        public KeszitesAdatok(IEnumerable<KeszitesAdat> keszitesAdatok)
        {
            adatok = keszitesAdatok.ToDictionary(k => k.Azonosito, k => k);
        }

        public KeszitesAdat this[string azonosito] => adatok[azonosito];

        public IEnumerable<string> ElerhetoKeszitesAzonositok =>
            adatok.Keys.OrderBy(a => a, StringComparer.Ordinal);
    }
}
