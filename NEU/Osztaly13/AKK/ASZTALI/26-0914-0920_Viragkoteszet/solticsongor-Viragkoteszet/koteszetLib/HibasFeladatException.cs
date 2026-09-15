namespace koteszetLib
{
    public class HibasFeladatException : Exception
    {
        public int DolgozoId { get; }
        public int TermekId { get; }

        public HibasFeladatException(int dolgozoId, int termekId)
            : base("A feladathoz nincs elegendő tudása a gyakornoknak.")
        {
            DolgozoId = dolgozoId;
            TermekId = termekId;
        }
    }
}