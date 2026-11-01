namespace csokigyarLib
{
    public class SilanyMinosegException : Exception
    {
        public SilanyMinosegException() : base("Nem igazi csokoládé!")
        {
            // ha nem megy, hát nem megy
        }
    }
}