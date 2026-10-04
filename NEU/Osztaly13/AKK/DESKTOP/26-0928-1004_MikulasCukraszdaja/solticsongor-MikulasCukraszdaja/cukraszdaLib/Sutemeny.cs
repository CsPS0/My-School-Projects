namespace cukraszdaLib
{
    public abstract class Sutemeny : IEtel
    {
        public string Azonosito { get; }
        public string Tipus { get; }
        public string Megnevezes { get; }
        protected KeszitesAdatok KeszitesAdatok { get; }

        public abstract int ElkeszitesiIdo { get; }

        protected Sutemeny(string azonosito, string tipus, string megnevezes, KeszitesAdatok keszitesAdatok)
        {
            Azonosito = azonosito;
            Tipus = tipus;
            Megnevezes = megnevezes;
            KeszitesAdatok = keszitesAdatok;
        }

        public override string ToString() => Megnevezes;
    }
}
