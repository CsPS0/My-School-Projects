namespace teremLib
{
    public abstract class Terem
    {
        public int Azonosito { get; set; }
        public int Ferohely { get; set; }
        public Orarend Orarend { get; set; }

        public Terem(int azon, int feroh)
        {
            Azonosito = azon;
            Ferohely = feroh;
            Orarend = new Orarend();
        }

        public abstract void IdopontFoglalas(Foglalas foglalas);
    }
}