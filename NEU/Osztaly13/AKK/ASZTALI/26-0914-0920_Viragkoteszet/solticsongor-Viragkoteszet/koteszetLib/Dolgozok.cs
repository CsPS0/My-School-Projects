using System.Collections;

namespace koteszetLib
{
    public class Dolgozok : IEnumerable<Dolgozo>
    {
        private readonly Dictionary<int, Dolgozo> _dolgozok;

        public Dolgozok(IEnumerable<string> dolgozoiAdatok)
        {
            _dolgozok = new Dictionary<int, Dolgozo>();
            foreach (var adat in dolgozoiAdatok)
            {
                var dolgozo = MunkaEroFelvetel.DolgozoLetrehozasa(adat);
                _dolgozok.Add(dolgozo.Id, dolgozo);
            }
        }

        public Dolgozo this[int id] => _dolgozok[id];

        public int Letszam => _dolgozok.Count;

        public IEnumerator<Dolgozo> GetEnumerator()
        {
            return _dolgozok.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}