namespace cukraszdaLib
{
    public class Feladat
    {
        public const int MaxPerc = 480;

        public Sutemeny Sutemeny { get; }
        public int Adag { get; }
        public int Darabszam => Adag;
        public int ElkeszitesiIdo => Sutemeny.ElkeszitesiIdo * Adag;

        public Feladat(Sutemeny sutemeny, int adag)
        {
            if (sutemeny.ElkeszitesiIdo * adag > MaxPerc)
            {
                throw new TulSokFeladatException();
            }

            Sutemeny = sutemeny;
            Adag = adag;
        }

        public override string ToString()
        {
            return $"{Sutemeny.Megnevezes}: {Adag} adag, elkészítési idő: {ElkeszitesiIdo} perc";
        }
    }
}
