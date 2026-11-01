using System.Text;

namespace koteszetLib
{
    public class Termekek
    {
        private readonly Dictionary<int, Termek> _termekek;

        public Termekek(IEnumerable<string> termekekSorai, Katalogus alapanyagokKatalogusa)
        {
            _termekek = new Dictionary<int, Termek>();
            foreach (var sor in termekekSorai)
            {
                var termek = new Termek(sor, alapanyagokKatalogusa);
                _termekek.Add(termek.Id, termek);
            }
        }

        public Termek this[int id] => _termekek[id];

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Elkészíthető termékek:");
            foreach (var termek in _termekek.Values)
            {
                sb.AppendLine(termek.ToString());
            }
            return sb.ToString();
        }
    }
}