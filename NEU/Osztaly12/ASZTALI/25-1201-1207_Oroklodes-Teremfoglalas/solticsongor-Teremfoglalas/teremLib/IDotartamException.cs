namespace teremLib
{
    public class IdotartamException : Exception
    {
        public IdotartamException() : base("A lefoglalt időtartam nem pozitív, vagy nem 15-tel osztható.")
        {
            // ha nem megy, nem megy
        }
    }
}