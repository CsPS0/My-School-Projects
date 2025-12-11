namespace teremLib
{
    public class FoglalasException : Exception
    {
        public FoglalasException() : base("A kért időpontban a terem nem foglalható.")
        {
            // ha nem megy, nem megy
        }
    }
}