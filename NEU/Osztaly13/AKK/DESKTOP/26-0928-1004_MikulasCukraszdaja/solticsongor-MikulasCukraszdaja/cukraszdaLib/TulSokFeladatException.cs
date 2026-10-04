namespace cukraszdaLib
{
    public class TulSokFeladatException : Exception
    {
        public TulSokFeladatException()
            : base("Túl sok feladat, több mint 8 óra elkészíteni.")
        {
        }

        public TulSokFeladatException(string message)
            : base(message)
        {
        }

        public TulSokFeladatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}