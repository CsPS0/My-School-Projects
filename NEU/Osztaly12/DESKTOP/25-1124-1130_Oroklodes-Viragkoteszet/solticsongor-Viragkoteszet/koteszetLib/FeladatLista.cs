namespace koteszetLib
{
    public class FeladatLista
    {
        public List<Termek> Feladatok { get; }

        public FeladatLista()
        {
            Feladatok = new List<Termek>();
        }

        private FeladatLista(List<Termek> feladatok)
        {
            Feladatok = feladatok;
        }

        public static FeladatLista operator +(FeladatLista feladatok, Termek ujFeladat)
        {
            var ujLista = new List<Termek>(feladatok.Feladatok);
            ujLista.Add(ujFeladat);
            return new FeladatLista(ujLista);
        }
    }
}