namespace cukraszdaLib
{
    public class Feladatok
    {
        private readonly List<Feladat> feladatLista;

        public Feladatok()
        {
            feladatLista = new List<Feladat>();
        }

        public Feladatok(IEnumerable<Feladat> feladatok)
        {
            feladatLista = new List<Feladat>(feladatok);
        }

        public IEnumerable<Feladat> FeladatLista => feladatLista.AsReadOnly();

        public static Feladatok operator +(Feladatok feladatok, Feladat ujFeladat)
        {
            var ujLista = new List<Feladat>(feladatok.FeladatLista)
            {
                ujFeladat
            };
            return new Feladatok(ujLista);
        }
    }
}
